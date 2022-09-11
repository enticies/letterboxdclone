using lbdbackend.Service.DTOs.NewsDTOs;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lbdbackend.Api.App.User.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService) {
            _newsService = newsService; 
        }

        [HttpPut]
        [Route("createnews")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Create([FromForm] NewsCreateDTO newsCreateDTO) {
            await _newsService.Create(newsCreateDTO);
            return Ok();
        }
        [HttpGet]
        [Route("getusernews")]
        public async Task<IActionResult> GetUserNews(string userName, int i = 1) {
            return Ok(await _newsService.GetUserNews(userName, i));
        }

        [HttpDelete]
        [Route("deletenews")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> DeleteNews(int id) {
            await _newsService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("getbyid")]
        public async Task<NewsGetDTO> GetById(int id) {
            return await _newsService.GetById(id);
        }

    }
}
