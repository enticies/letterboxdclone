using lbdbackend.Core.Entities;
using lbdbackend.Service.DTOs.GenreDTOs;
using lbdbackend.Service.DTOs.MovieDTOs;
using lbdbackend.Service.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Interfaces {
    public interface IUserService {
        Task<UserGetDTO> GetUserMain(string userName);
        Task<bool> Follow(string followerId, string followeeId);
        Task<bool> CheckFollow(string followerUsername, string followeeUsername);
        Task<PaginatedListDTO<UserGetDTO>> GetUserFollowers(string userName, int i);
        Task<PaginatedListDTO<UserGetDTO>> GetUserFollowees(string userName, int i);
        Task ChangeUserImage(UserImageDTO userImageDTO);
        Task ChangeUserCredentials(string userName, UserChangeDTO userChangeDTO);
        Task<List<UserGetDTO>> GetRecentMembers(int quantity = 5);
        Task<PaginatedListDTO<UserGetDTO>> GetPaginatedUsers(int i = 1);
        Task<PaginatedListDTO<UserGetDTO>> GetPaginatedUsers(string s, int i = 1);
    }
}