using Amazon.SimpleSystemsManagement.Model;
using AutoMapper;
using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using lbdbackend.Service.DTOs.ListDTOs;
using lbdbackend.Service.DTOs.MovieDTOs;
using lbdbackend.Service.Exceptions;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Services {
    public class MovieListService : IMovieListService {
        private readonly IJoinMoviesListsRepository _repo;
        private readonly IMovieListRepository _movieListRepo;
        private readonly IJoinMoviesListsRepository _joinMoviesListsRepo;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public MovieListService(IJoinMoviesListsRepository repo, IMapper mapper, IReviewRepository reviewRepo, UserManager<AppUser> userManager, IMovieListRepository movieListRepository, IMovieRepository movieRepository, IJoinMoviesListsRepository joinMoviesListsRepository) {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _movieListRepo = movieListRepository;
            _movieRepository = movieRepository;
            _joinMoviesListsRepo = joinMoviesListsRepository;   
        }

        public async Task Create(MovieListCreateDTO movieListCreateDTO) {
            if (await _userManager.FindByNameAsync(movieListCreateDTO.OwnerUsername) == null) {
                throw new ItemNotFoundException("Username not found.");
            }
            foreach (int movieId in movieListCreateDTO.Movies) {
                if (!await _movieRepository.ExistsAsync(m => m.ID == movieId)) {
                    throw new ItemNotFoundException("Movie not found");
                }
            }

            MovieList movieList = _mapper.Map<MovieList>(movieListCreateDTO);
            var user = await _userManager.FindByNameAsync(movieListCreateDTO.OwnerUsername);
            if (await _movieListRepo.ExistsAsync(l => l.OwnerId == user.Id && l.Name == movieListCreateDTO.Name)) {
                throw new AlreadyExistsException("List already exists.");
            }
            movieList.OwnerId = user.Id;
            movieList.MovieCount = movieListCreateDTO.Movies.Count;
            await _movieListRepo.AddAsync(movieList);
            await _repo.CommitAsync();

            foreach (int movieId in movieListCreateDTO.Movies) {
                var row = new JoinMoviesLists();
                if (!await _movieRepository.ExistsAsync(m => m.ID == movieId)) {
                    throw new ItemNotFoundException("Movie not found");
                }
                row.MovieListId = movieList.ID;
                row.MovieId = movieId;
                await _repo.AddAsync(row);
            }


            await _movieListRepo.CommitAsync();
        }

        public async Task<List<MovieGetDTO>> GetListMovies(int id) {
            if (!await _movieListRepo.ExistsAsync(l => l.ID == id)) {
                throw new ItemNotFoundException("List not found.");
            }
            List<MovieGetDTO> movieGetDTOs = new List<MovieGetDTO>();

            foreach (var movieList in await _joinMoviesListsRepo.GetAllAsync(l => l.MovieListId == id, "Movie")) {
                var dto = _mapper.Map<MovieGetDTO>(movieList.Movie);
                movieGetDTOs.Add(dto);
            }

            return movieGetDTOs;
        }

        public async Task<List<MovieListGetDTO>> GetRecentsLists() {
            List<MovieListGetDTO> movieListGetDTOs = new List<MovieListGetDTO>();

            List<MovieList> movieLists = await _movieListRepo.GetAllAsync(r => !r.IsDeleted, "Owner");

            for (int i = Math.Max(0, movieLists.Count - 3); i < movieLists.Count; ++i) {
                var dto = _mapper.Map<MovieListGetDTO>(movieLists[i]);
                dto.OwnerUsername = movieLists[i].Owner.UserName;
                movieListGetDTOs.Add(dto);
            }

            return movieListGetDTOs;
        }

        public async Task<PaginatedListDTO<MovieListGetDTO>> GetUserLists(string userName, int i) {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) {
                throw new ItemNotFoundException("User not found.");
            }

            List<MovieListGetDTO> movieListGetDTOs = new List<MovieListGetDTO>();

            var movieLists = await _movieListRepo.GetAllAsync(l => !l.IsDeleted && l.OwnerId == user.Id);

            foreach (var movieList in movieLists) {
                //var dto = _mapper.Map<MovieListGetDTO>(movieList);
                var dto = new MovieListGetDTO();
                dto.OwnerUsername = user.UserName;
                dto.MovieCount = movieList.MovieCount;
                dto.Name = movieList.Name;
                dto.Id = movieList.ID;
                movieListGetDTOs.Add(dto);
            }

            PaginatedListDTO<MovieListGetDTO> paginatedListDTO = new PaginatedListDTO<MovieListGetDTO>(movieListGetDTOs, i, 10);

            return paginatedListDTO;
        }

        public async Task Delete(int id) {
            if (!await _movieListRepo.ExistsAsync(n => !n.IsDeleted && n.ID == id)) {
                throw new ItemNotFoundException("News not found.");
            }
            var list = await _movieListRepo.GetAsync(n => !n.IsDeleted && n.ID == id);
            list.IsDeleted = true;
            await _repo.CommitAsync();
        }





    }
}
