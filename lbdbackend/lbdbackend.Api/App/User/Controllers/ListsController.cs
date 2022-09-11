using lbdbackend.Service.DTOs.ListDTOs;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lbdbackend.Api.App.User.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase {
        private readonly IMovieListService _movieListService;
        public ListsController(IMovieListService movieListService) {
            _movieListService = movieListService;       
        }

        [HttpPut]
        [Route("createlist")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Create(MovieListCreateDTO movieListCreateDTO) {
            await _movieListService.Create(movieListCreateDTO);
            return StatusCode(201);
        }
        [HttpGet]
        [Route("getuserlists")]
        public async Task<IActionResult> GetUserLists(string userName, int i) {
            return Ok(await _movieListService.GetUserLists(userName, i));
        }

        [HttpGet]
        [Route("getlistmovies")]
        public async Task<IActionResult> GetListMovies(int id) {
            return Ok(await _movieListService.GetListMovies(id));
        }

        [HttpDelete]
        [Authorize(Roles = "Superadmin, Admin, Member")]

        public async Task<IActionResult> Delete(int id) {
            await _movieListService.Delete(id);
            return Ok();
        }


    }
}
