using lbdbackend.Service.DTOs.GenreDTOs;
using lbdbackend.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using lbdbackend.Service.Interfaces;
using System;
using lbdbackend.Service.DTOs.ProfessionDTOs;
using Microsoft.AspNetCore.Authorization;

namespace lbdbackend.Api.App.Admin.Controllers {
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Superadmin, Admin")]
    [ApiController]
    public class ProfessionsController : ControllerBase {
        private readonly IProfessionsService _professionService;
        public ProfessionsController(IProfessionsService professionService) {
            _professionService = professionService;
        }
        [HttpPut]
        [Route("Create")]
        public async Task<IActionResult> Create(ProfessionCreateDTO professionCreateDTO) {
            await _professionService.Create(professionCreateDTO);
            return StatusCode(201);
        }

        [HttpPost]
        [Route("DeleteOrRestore")]
        public async Task<IActionResult> DeleteOrRestore(int? id) {
            await _professionService.DeleteOrRestore(id);
            return StatusCode(204);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(int? id, ProfessionUpdateDTO professionUpdateDTO) {
            await _professionService.Update(id, professionUpdateDTO);
            return StatusCode(204);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll() {
            return Ok(await _professionService.GetProfessions());
        }

        [HttpGet]
        [Route("GetByID")]
        public async Task<IActionResult> GetByID(int? id) {
            return Ok(await _professionService.GetByID(id));
        }



    }
}