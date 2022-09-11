using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lbdbackend.Api.App.User.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase {
        private readonly IMovieService _movieService;
        private readonly IReviewService _reviewService;
        public FilmController(IMovieService movieService, IReviewService reviewService) {
            _movieService = movieService;
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var recentMovies = await _movieService.GetRecentMovies();
            var recentReviews = await _reviewService.GetRecentReviews(8);
            return Ok(new {
                recentMovies = recentMovies,
                recentReviews = recentReviews,
            }
               );
        }
    }
}