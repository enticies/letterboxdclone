using AutoMapper;
using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using lbdbackend.Service.DTOs.CommentDTOs;
using lbdbackend.Service.DTOs.GenreDTOs;
using lbdbackend.Service.Exceptions;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using P225NLayerArchitectura.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Services {
    public class CommentService : ICommentService {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _repo;
        private readonly IReviewRepository _reviewRepo;
        private readonly UserManager<AppUser> _userManager;
        public CommentService(ICommentRepository repo, IMapper mapper, IReviewRepository reviewRepo, UserManager<AppUser> userManager) {
            _repo = repo;
            _mapper = mapper;
            _reviewRepo = reviewRepo;
            _userManager = userManager;
        }

        public async Task CreateComment(CommentCreateDTO commentCreateDTO) {
            if (!await _reviewRepo.ExistsAsync(e => e.ID == commentCreateDTO.ReviewID)) {
                throw new ItemNotFoundException("Review ID doesn't exist.");
            }
            if (await _userManager.FindByIdAsync(commentCreateDTO.OwnerId) == null) {
                throw new ItemNotFoundException("User ID doesn't exist.");
            }
            Comment comment = _mapper.Map<Comment>(commentCreateDTO);
            Review review = await _reviewRepo.GetAsync(r => r.ID == commentCreateDTO.ReviewID);
            review.CommentCount += 1;
            await _repo.AddAsync(comment);
            await _repo.CommitAsync();
        }

        public async Task<List<CommentGetDTO>> GetReviewComments(int? reviewID) {
            if (reviewID == null) {
                throw new ArgumentNullException();
            }

            if (!await _reviewRepo.ExistsAsync(r => r.ID == reviewID)) {
                throw new ItemNotFoundException("Review not found.");
            }

            List<CommentGetDTO> dtos = new List<CommentGetDTO>();

            foreach (Comment comment in await _repo.GetAllAsync(c => !c.IsDeleted && c.ReviewId == reviewID, "Owner")) {
                var dto = _mapper.Map<CommentGetDTO>(comment);
                dto.Username = comment.Owner.UserName;
                dtos.Add(dto);
            }

            return dtos;
        }
        public async Task Delete(int? id) {
            if (id == null) {
                throw new BadRequestException("ID can't be null.");
            }
            if (!await _repo.ExistsAsync(e => e.ID == id)) {
                throw new ItemNotFoundException("ID not found.");
            }

            Comment comment = await _repo.GetAsync(c => c.ID == id);
            comment.IsDeleted = true;
            comment.DeletedAt = DateTime.UtcNow.AddHours(4);

            Review review = await _reviewRepo.GetAsync(r => r.ID == comment.ReviewId);
            review.CommentCount -= 1;
            await _repo.CommitAsync();
        }

    }
}