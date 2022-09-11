using lbdbackend.Service.DTOs.GenreDTOs;
using lbdbackend.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using lbdbackend.Service.Interfaces;
using System;
using lbdbackend.Service.DTOs.MovieDTOs;
using Microsoft.AspNetCore.Authorization;

namespace lbdbackend.Api.App.Admin.Controllers {
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Superadmin, Admin")]
    [ApiController]
    public class MoviesController : ControllerBase {
        private readonly IMovieService _movieService;
        private readonly IYearsService _yearsService;
        public MoviesController(IMovieService movieService, IYearsService yearsService) {
            _movieService = movieService;
            _yearsService = yearsService;   
        }
        [HttpPut]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] MovieCreateDTO movieCreateDTO) {
            await _movieService.Create(movieCreateDTO);
            return StatusCode(201);
        }

        [HttpPost]
        [Route("DeleteOrRestore")]
        public async Task<IActionResult> DeleteOrRestore(int? id) {
            await _movieService.DeleteOrRestore(id);
            return StatusCode(204);
        }
        [HttpPost]
        [Route("Update")]

        public async Task<IActionResult> Update(int? id, [FromForm] MovieUpdateDTO movieUpdateDTO) {
            await _movieService.Update(id, movieUpdateDTO);
            return StatusCode(204);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll() {
            return Ok(await _movieService.GetMovies());
        }
        [HttpGet]
        [Route("GetYears")]
        public async Task<IActionResult> GetYears() {
            return Ok(await _yearsService.GetYears());
        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<IActionResult> GetByID(int? id) {
            return Ok(await _movieService.GetByID(id));
        }

    }
}
