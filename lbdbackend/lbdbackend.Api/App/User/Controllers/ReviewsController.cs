using lbdbackend.Service.DTOs.ReviewDTOs;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lbdbackend.Api.App.User.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService) {
            _reviewService = reviewService;
        }

        [HttpPut]
        [Route("create")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Create(ReviewCreateDTO reviewCreateDTO) {
            await _reviewService.Create(reviewCreateDTO);
            return StatusCode(201);
        }

        [HttpGet]
        [Route("getmoviereviews")]
        public async Task<IActionResult> GetMoviesPaginated(int movieID, int i = 1) {
            return Ok(await _reviewService.GetPaginatedReviews(movieID, i));
        }

        [HttpGet]
        [Route("getreview")]
        public async Task<IActionResult> GetReview(int reviewID) {
            return Ok(await _reviewService.GetReview(reviewID));
        }        
        [HttpGet]
        [Route("getrecentreviews")]
        public async Task<IActionResult> GetRecentReviews(string userName) {
            return Ok(await _reviewService.GetRecentReviews(userName));
        }
        [HttpGet]
        [Route("getuserreviews")]
        public async Task<IActionResult> GetUserReviews(string userName, int i = 1) {
            return Ok(await _reviewService.GetPaginatedUserReviews(userName, i));
        }
        [HttpGet]
        [Route("getalluserreviews")]
        public async Task<IActionResult> GetAllUserReviews(string userName, int i = 1) {
            return Ok(await _reviewService.GetAllUserReviews(userName, i));
        }

        [HttpDelete]
        [Route("deletereview")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> DeleteReview(int? id) {
            await _reviewService.DeleteReview(id);
            return Ok();
        }







    }
}
