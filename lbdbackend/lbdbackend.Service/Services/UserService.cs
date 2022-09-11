using AutoMapper;
using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using lbdbackend.Service.DTOs.GenreDTOs;
using lbdbackend.Service.DTOs.MovieDTOs;
using lbdbackend.Service.DTOs.UserDTOs;
using lbdbackend.Service.Exceptions;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P225Allup.Extensions;
using P225NLayerArchitectura.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Services {
    public class UserService : IUserService {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _repo;
        private readonly IRelationshipRepository _relationshipRepo;
        private readonly IReviewRepository _reviewRepo;
        private readonly IMovieListRepository _movieListRepository;
        private readonly IMovieListService _movieListService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public UserService(UserManager<AppUser> repo, IMapper mapper, IRelationshipRepository relationshipRepository, IReviewRepository reviewRepository, UserManager<AppUser> userManager, IMovieListService movieListService, IMovieListRepository movieListRepository, IWebHostEnvironment env) {
            _repo = repo;
            _mapper = mapper;
            _relationshipRepo = relationshipRepository;
            _reviewRepo = reviewRepository;
            _userManager = userManager;
            _movieListService = movieListService;
            _movieListRepository = movieListRepository;
            _env = env;
        }

        public async Task<bool> CheckFollow(string followerUsername, string followeeUsername) {
            if (followerUsername == followeeUsername) {
                throw new BadRequestException("Usernames can't be same.");
            }

            var follower = await _repo.FindByNameAsync(followerUsername);
            var followee = await _repo.FindByNameAsync(followeeUsername);

            if (follower == null) {
                throw new ItemNotFoundException("Follower id not found.");
            }

            if (followee == null) {
                throw new ItemNotFoundException("Followee id not found.");
            }
            if (await _relationshipRepo.ExistsAsync(r => !r.IsDeleted && r.FollowerId == follower.Id && r.FolloweeId == followee.Id)) {
                return true;
            }
            return false;
        }

        public async Task<bool> Follow(string followerUsername, string followeeUsername) {
            if (followerUsername == followeeUsername) {
                throw new BadRequestException("Usernames can't be same.");
            }

            bool isFollowing = false;

            var follower = await _repo.FindByNameAsync(followerUsername);
            var followee = await _repo.FindByNameAsync(followeeUsername);

            if (follower == null) {
                throw new ItemNotFoundException("Follower id not found.");
            }

            if (followee == null) {
                throw new ItemNotFoundException("Followee id not found.");
            }

            Relationship relationship = new Relationship();
            if (await _relationshipRepo.ExistsAsync(r => r.FollowerId == follower.Id && r.FolloweeId == followee.Id)) {
                var row = _relationshipRepo.GetAsync(r => r.FollowerId == follower.Id && r.FolloweeId == followee.Id).Result;
                if (row.IsDeleted) {
                    row.IsDeleted = false;
                    isFollowing = true;
                }
                else {
                    row.IsDeleted = true;
                    isFollowing = false;
                }
            }
            else {
                relationship.FollowerId = follower.Id;
                relationship.FolloweeId = followee.Id;
                await _relationshipRepo.AddAsync(relationship);
                isFollowing = true;
            }


            await _relationshipRepo.CommitAsync();
            return isFollowing;
        }

        public async Task<PaginatedListDTO<UserGetDTO>> GetUserFollowers(string userName, int i) {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) {
                throw new ItemNotFoundException("User not found.");
            }

            List<UserGetDTO> userGetDTOs = new List<UserGetDTO>();

            var followers = await _relationshipRepo.GetAllAsync(f => !f.IsDeleted && f.FolloweeId == user.Id, "Follower");

            foreach (var f in followers) {
                var dto = new UserGetDTO();
                dto.Image = f.Follower.Image;
                dto.UserName = f.Follower.UserName;
                userGetDTOs.Add(dto);
            }

            PaginatedListDTO<UserGetDTO> paginatedListDTO = new PaginatedListDTO<UserGetDTO>(userGetDTOs, i, 5);


            return paginatedListDTO;
        }

        public async Task<PaginatedListDTO<UserGetDTO>> GetUserFollowees(string userName, int i) {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) {
                throw new ItemNotFoundException("User not found.");
            }

            List<UserGetDTO> userGetDTOs = new List<UserGetDTO>();

            var followees = await _relationshipRepo.GetAllAsync(f => !f.IsDeleted && f.FollowerId == user.Id, "Followee");

            foreach (var f in followees) {
                var dto = new UserGetDTO();
                dto.Image = f.Followee.Image;
                dto.UserName = f.Followee.UserName;
                userGetDTOs.Add(dto);
            }

            PaginatedListDTO<UserGetDTO> paginatedListDTO = new PaginatedListDTO<UserGetDTO>(userGetDTOs, i, 5);


            return paginatedListDTO;
        }

        public async Task<UserGetDTO> GetUserMain(string userName) {
            var user = await _repo.FindByNameAsync(userName);

            if (user == null) {
                throw new ItemNotFoundException("User not found.");
            }
            string userId = user.Id;

            int followeeCount = await _relationshipRepo.GetCount(e => !e.IsDeleted && e.FollowerId == userId);
            int followerCount = await _relationshipRepo.GetCount(e => !e.IsDeleted && e.FolloweeId == userId);
            int listCount = await _movieListRepository.GetCount(e => !e.IsDeleted && e.OwnerId == userId);


            UserGetDTO userGetDTO = new UserGetDTO();
            userGetDTO.FolloweeCount = followeeCount;
            userGetDTO.FollowerCount = followerCount;
            userGetDTO.ListCount = listCount;


            List<int> movieIds = new List<int>();

            foreach (Review review in await _reviewRepo.GetAllAsync(r => !r.IsDeleted && r.OwnerId == userId)) {
                movieIds.Add(review.MovieId);
            }

            int movieCount = movieIds.Distinct().Count();

            userGetDTO.FilmCount = movieCount;
            userGetDTO.Image = user.Image;

            return userGetDTO;
        }

        public async Task ChangeUserImage(UserImageDTO userImageDTO) {
            if (userImageDTO.Image == null) {
                throw new ArgumentNullException();
            }
            var user = await _userManager.FindByNameAsync(userImageDTO.UserName);

            if (user == null) {
                throw new ItemNotFoundException("User not found;");
            }

            user.Image = await userImageDTO.Image.CreateFileAsync(_env, "images", "users");
            await _repo.UpdateAsync(user);
            await _movieListRepository.CommitAsync();
        }

        public async Task ChangeUserCredentials(string userName, UserChangeDTO userChangeDTO) {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) {
                throw new ItemNotFoundException("User not found;");
            }

            if (userChangeDTO == null) {
                throw new ArgumentNullException();
            }

            if (userChangeDTO.UserName.Length < 6) {
                throw new BadRequestException("Username can't be smaller than 6 characters.");
            }

            if (userChangeDTO.Password.Length < 6) {
                throw new BadRequestException("Password can't be smaller than 6 characters.");
            }

            if (userName != userChangeDTO.UserName) {
                if (await _userManager.FindByNameAsync(userChangeDTO.UserName) != null || await _userManager.FindByEmailAsync(userChangeDTO.Email) != null) {
                    throw new AlreadyExistException("User already exists.");
                }
            }

                user.UserName = userChangeDTO.UserName;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, userChangeDTO.Password);

            user.Email = userChangeDTO.Email;

            await _repo.UpdateAsync(user);
            await _movieListRepository.CommitAsync();

        }

        public async Task<List<UserGetDTO>> GetRecentMembers(int quantity = 5) {
            List<UserGetDTO> userGetDTOs = new List<UserGetDTO>();

            List<AppUser> users = await _userManager.Users.ToListAsync();

            var members = new List<AppUser>();

            foreach (var user in users) {
                if ((await _userManager.GetRolesAsync(user)).ToList().Exists(e => e == "Member")) {
                    members.Add(user);
                }
            }

            for (int i = Math.Max(0, members.Count - quantity); i < members.Count; ++i) {
                var dto = _mapper.Map<UserGetDTO>(members[i]);
                var reviewCount = await _reviewRepo.GetCount(r => !r.IsDeleted && r.OwnerId == members[i].Id);
                dto.ReviewCount = reviewCount;
                userGetDTOs.Add(dto);
            }

            return userGetDTOs;
        }

        public async Task<PaginatedListDTO<UserGetDTO>> GetPaginatedUsers(int i) {
            List<UserGetDTO> userGetDTOs = new List<UserGetDTO>();

            List<AppUser> reviews = new List<AppUser>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users) {
                if ((await _userManager.GetRolesAsync(user)).ToList().Exists(e => e == "Member")) {
                    var dto = _mapper.Map<UserGetDTO>(user);
                    userGetDTOs.Add(dto);
                }
            }
            PaginatedListDTO<UserGetDTO> paginatedListDTO = new PaginatedListDTO<UserGetDTO>(userGetDTOs, i, 6);

            return paginatedListDTO;
        }

        public async Task<PaginatedListDTO<UserGetDTO>> GetPaginatedUsers(string s, int i) {
            List<UserGetDTO> userGetDTOs = new List<UserGetDTO>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var item in users) {
                if (item.UserName.Contains(s) && (await _userManager.GetRolesAsync(item)).ToList().Exists(e => e == "Member")) {
                    var dto = _mapper.Map<UserGetDTO>(item);
                    userGetDTOs.Add(dto);
                }
            }
            PaginatedListDTO<UserGetDTO> paginatedListDTO = new PaginatedListDTO<UserGetDTO>(userGetDTOs, i, 2);

            return paginatedListDTO;
        }

    }
}