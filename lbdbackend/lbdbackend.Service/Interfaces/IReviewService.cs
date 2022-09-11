using lbdbackend.Service.DTOs.MovieDTOs;
using lbdbackend.Service.DTOs.ReviewDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Interfaces {
    public interface IReviewService {
        Task Create(ReviewCreateDTO reviewCreateDTO);
        Task<List<ReviewGetDTO>> GetMovieReviews(int? movieID);
        Task<PaginatedListDTO<ReviewGetDTO>> GetPaginatedReviews(int movieID, int i);
        Task<ReviewGetDTO> GetReview(int reviewID);
        Task<List<ReviewGetDTO>> GetRecentReviews(string userName);
        Task<List<ReviewGetDTO>> GetRecentReviews(int quantity = 5);
        Task<PaginatedListDTO<ReviewGetDTO>> GetPaginatedUserReviews(string userName, int i);
        Task<PaginatedListDTO<ReviewGetDTO>> GetAllUserReviews(string userName, int i);
        Task DeleteReview(int? id);

    }
}