using lbdbackend.Service.DTOs.GenreDTOs;
using lbdbackend.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using lbdbackend.Service.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace lbdbackend.Api.App.Admin.Controllers {
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Superadmin, Admin")]
    [ApiController]
    public class GenresController : ControllerBase {
        private readonly IGenresService _genreService;
        public GenresController(IGenresService genreService) {
            _genreService = genreService;   
        }
        [HttpPut]
        [Route("Create")]
        public async Task<IActionResult> Create(GenreCreateDTO genreCreateDTO) {
            await _genreService.Create(genreCreateDTO);
            return StatusCode(201);
        }

        [HttpPost]
        [Route("DeleteOrRestore")]
        public async Task<IActionResult> DeleteOrRestore(int? id) {
            await  _genreService.DeleteOrRestore(id);
            return StatusCode(204);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(int? id, GenreUpdateDTO genreUpdateDTO) {
            await _genreService.Update(id, genreUpdateDTO);
            return StatusCode(204);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll() {
            return Ok(await _genreService.GetGenres(null));
        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<IActionResult> GetByID(int? id) {
            return Ok(await _genreService.GetByID(id));
        }

    }
}
