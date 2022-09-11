using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace lbdbackend.Api.App.User.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class MoviesController : ControllerBase {
        private readonly IMovieService _movieService;
        private readonly IYearsService _yearsService;
        private readonly IGenresService _genresService;
        public MoviesController(IMovieService movieService, IYearsService yearsService, IGenresService genresService) {
            _movieService = movieService;
            _yearsService = yearsService;
            _genresService = genresService;
        }

        [HttpGet]
        [Route("getyears")]
        public async Task<IActionResult> GetYears() {
            return Ok(await _yearsService.GetYears());
        }

        [HttpPost]
        [Route("findmovie")]
        public async Task<IActionResult> FindMovieByString(string str) {
            return Ok(await _movieService.GetByStr(str));
        }

        [HttpGet]
        [Route("getbyid")]
        public async Task<IActionResult> GetByID(int? id) {
            return Ok(await _movieService.GetByID(id));
        }
        [HttpGet]
        [Route("searchmovies")]
        public async Task<IActionResult> GetMoviesPaginated(string s, int i = 1) {
            return Ok(await _movieService.GetAllPageIndexAsync(s, i));
        }

        [HttpGet]
        [Route("getmoviegenres")]
        public async Task<IActionResult> GetMovieGenres(int? id) {
            return Ok(await _genresService.GetGenres(id));
        }

    }
}
