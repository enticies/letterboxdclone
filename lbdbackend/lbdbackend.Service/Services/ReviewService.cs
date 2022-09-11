using AutoMapper;
using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using lbdbackend.Service.DTOs.MovieDTOs;
using lbdbackend.Service.DTOs.ReviewDTOs;
using lbdbackend.Service.Exceptions;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using P225NLayerArchitectura.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Services {
    public class ReviewService : IReviewService {
        private readonly IReviewRepository _repo;
        private readonly IMovieRepository _movieRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepository repo, IMapper mapper, IMovieRepository movieRepo, UserManager<AppUser> userManager) {
            _repo = repo;
            _mapper = mapper;
            _movieRepo = movieRepo; 
            _userManager = userManager;
        }
        public async Task Create(ReviewCreateDTO reviewCreateDTO) {
            if (!await _movieRepo.ExistsAsync(e => e.ID == reviewCreateDTO.MovieID)) {
                throw new ItemNotFoundException($"Movie ID doesn't exist.");
            }
            if (await _userManager.FindByIdAsync(reviewCreateDTO.OwnerID) == null) {
                throw new ItemNotFoundException($"User ID doesn't exist.");
            }
            if (await _repo.ExistsAsync(r => r.MovieId == reviewCreateDTO.MovieID && r.Body == reviewCreateDTO.Body && r.Rating == reviewCreateDTO.Rating)) {
                throw new AlreadyExistException("Review already exists");
            }
            Review review = _mapper.Map<Review>(reviewCreateDTO);
            review.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(review);
            await _repo.CommitAsync();
        }

        public async Task<List<ReviewGetDTO>> GetMovieReviews(int? movieID) {
            if (movieID == null) {
                throw new ArgumentNullException();
            }
            if (!await _movieRepo.ExistsAsync(m => m.ID == movieID)) {
                throw new ItemNotFoundException("Movie ID not found.");
            }

            List<ReviewGetDTO> reviews = new List<ReviewGetDTO>();

            foreach (Review review in await _repo.GetAllAsync(e => !e.IsDeleted && e.MovieId == movieID && e.Body.Trim().Length > 0, "Owner")) {
                var dto = _mapper.Map<ReviewGetDTO>(review);
                dto.Username = review.Owner.UserName;
                dto.Image = review.Owner.Image;
                reviews.Add(dto);
            }

            return reviews;
        }
        public async Task<PaginatedListDTO<ReviewGetDTO>> GetPaginatedReviews(int movieID, int i) {
            List<ReviewGetDTO> reviewGetDTOs = new List<ReviewGetDTO>();
            foreach (var item in await _repo.GetAllAsync(c => !c.IsDeleted && movieID == c.MovieId, "Owner")) {
                var dto = _mapper.Map<ReviewGetDTO>(item);
                dto.Username = item.Owner.UserName;
                dto.Image = item.Owner.Image;
                reviewGetDTOs.Add(dto);
            }
            PaginatedListDTO<ReviewGetDTO> paginatedListDTO = new PaginatedListDTO<ReviewGetDTO>(reviewGetDTOs, i, 8);

            return paginatedListDTO;
        }

        public async Task<PaginatedListDTO<ReviewGetDTO>> GetPaginatedUserReviews(string userName, int i) {
            if (await _userManager.FindByNameAsync(userName) == null) {
                throw new ItemNotFoundException("User not found.");
            }
            List<ReviewGetDTO> reviewGetDTOs = new List<ReviewGetDTO>();
            var user = await _userManager.FindByNameAsync(userName);

            List<Review> reviews = new List<Review>();

            foreach (var item in await _repo.GetAllAsync(c => !c.IsDeleted && c.OwnerId == user.Id, "Owner", "Movie")) {
                if (!reviews.Exists(r => r.MovieId == item.MovieId)) {
                    reviews.Add(item);
                }
            }
            foreach (var review in reviews) {
                var dto = _mapper.Map<ReviewGetDTO>(review);
                dto.Username = review.Owner.UserName;
                dto.Image = review.Movie.PosterImage;
                reviewGetDTOs.Add(dto);
            }
            PaginatedListDTO<ReviewGetDTO> paginatedListDTO = new PaginatedListDTO<ReviewGetDTO>(reviewGetDTOs, i, 10);

            return paginatedListDTO;
        }
        public async Task<PaginatedListDTO<ReviewGetDTO>> GetAllUserReviews(string userName, int i) {
            if (await _userManager.FindByNameAsync(userName) == null) {
                throw new ItemNotFoundException("User not found.");
            }
            List<ReviewGetDTO> reviewGetDTOs = new List<ReviewGetDTO>();
            var user = await _userManager.FindByNameAsync(userName);

            List<Review> reviews = new List<Review>();

            foreach (var item in await _repo.GetAllAsync(c => !c.IsDeleted && c.Body.Length > 0 && c.OwnerId == user.Id, "Owner", "Movie")) {
                var dto = _mapper.Map<ReviewGetDTO>(item);
                dto.Username = item.Owner.UserName;
                dto.Image = item.Movie.PosterImage;
                dto.OwnerImage = item.Owner.Image;
                dto.MovieName = item.Movie.Name;
                reviewGetDTOs.Add(dto);
            }
            PaginatedListDTO<ReviewGetDTO> paginatedListDTO = new PaginatedListDTO<ReviewGetDTO>(reviewGetDTOs, i, 10);

            return paginatedListDTO;
        }

        public async Task<List<ReviewGetDTO>> GetRecentReviews(string userName) {
            if (await _userManager.FindByNameAsync(userName) == null) {
                throw new ItemNotFoundException("User not found.");
            }
            List<ReviewGetDTO> reviewGetDTOs = new List<ReviewGetDTO>();
            var user = await _userManager.FindByNameAsync(userName);

            List<Review> reviews = await _repo.GetAllAsync(r => !r.IsDeleted && r.OwnerId == user.Id, "Movie");

            for (int i = Math.Max(0, reviews.Count - 4); i < reviews.Count; ++i) {
                var dto = _mapper.Map<ReviewGetDTO>(reviews[i]);

                dto.Image = reviews[i].Movie.PosterImage;
                reviewGetDTOs.Add(dto);
            }

            return reviewGetDTOs;
        }

        public async Task<List<ReviewGetDTO>> GetRecentReviews(int quantity = 5) {
            List<ReviewGetDTO> reviewGetDTOs = new List<ReviewGetDTO>();

            List<Review> reviews = await _repo.GetAllAsync(r => !r.IsDeleted && r.Body.Trim().Length > 0, "Movie", "Owner");

            for (int i = Math.Max(0, reviews.Count - quantity); i < reviews.Count; ++i) {
                var dto = _mapper.Map<ReviewGetDTO>(reviews[i]);
                dto.Username = reviews[i].Owner.UserName;
                dto.Image = reviews[i].Movie.PosterImage;
                reviewGetDTOs.Add(dto);
            }

            return reviewGetDTOs;
        }

        public async Task<ReviewGetDTO> GetReview(int reviewID) {
            if (!await _repo.ExistsAsync(r => r.ID == reviewID)) {
                throw new ItemNotFoundException("Review not found.");
            }

            var review = await _repo.GetAsync(r => !r.IsDeleted && r.ID == reviewID, "Owner", "Movie");
            var dto = _mapper.Map<ReviewGetDTO>(review);
            dto.Username = review.Owner.UserName;
            dto.Image = review.Movie.PosterImage;
            return dto;
        }

        public async Task DeleteReview(int? id) {
            if (id == null) {
                throw new ArgumentNullException();
            }

            if (!await _repo.ExistsAsync(e => e.ID == id)) {
                throw new ItemNotFoundException("Review not found.");
            }

            var review = await _repo.GetAsync(e => e.ID == id);
            review.IsDeleted = true;

            await _repo.CommitAsync();
        }
    }
}
