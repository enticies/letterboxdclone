using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lbdbackend.Api.App.User.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase {
        private readonly IReviewService _reviewService;
        private readonly INewsService _newsService;
        private readonly IMovieListService _movieListService;
        public HomeController(IReviewService reviewService, INewsService newsService, IMovieListService movieListService) {
            _reviewService = reviewService;
            _newsService = newsService;
            _movieListService = movieListService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var reviews = await _reviewService.GetRecentReviews();
            var latestNews = await _newsService.GetLatestNews();
            var recentNews = await _newsService.GetRecentNews();
            var recentLists = await _movieListService.GetRecentsLists();
            return Ok(new {
                reviews = reviews,
                latestNews = latestNews,
                recentNews = recentNews,
                recentLists = recentLists,
            });

        }

    }
}
