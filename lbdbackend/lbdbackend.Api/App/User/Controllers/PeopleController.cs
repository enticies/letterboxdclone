using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lbdbackend.Api.App.User.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase {
        private readonly IPersonService _personService;
        private readonly IMovieService _movieService;

        public PeopleController(IPersonService personService, IMovieService movieService) {
            _personService = personService;
            _movieService = movieService;
        }

        [HttpGet]
        [Route("getmoviepeople")]
        public async Task<IActionResult> GetMoviePeople(int? id) {
            var people = await _personService.GetMoviePeople(id);
            return Ok(people);
        }
        [HttpGet]
        [Route("getpersonpage")]
        public async Task<IActionResult> GetPersonPage(int id) {
            var person = await _personService.GetByID(id);
            var movies = await _movieService.GetPersonMovies(id);
            return Ok(new { 
                person = person,
                movies = movies

            });
        }
    }
}
