using lbdbackend.Service.DTOs.CommentDTOs;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lbdbackend.Api.App.User.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService) {
            _commentService = commentService;   
        }
        [HttpPut]
        [Route("create")]
        [Authorize(Roles = "Member")]

        public async Task<IActionResult> Create(CommentCreateDTO createCommentDTO) {
            await _commentService.CreateComment(createCommentDTO);
            return StatusCode(201);
        }
        [HttpGet]
        [Route("getreviewcomments")]
        public async Task<IActionResult> GetReviewComments(int? reviewID) {
            return Ok(await _commentService.GetReviewComments(reviewID));
        }
        [HttpPost]
        [Route("deletecomment")]
        [Authorize(Roles = "Member, Admin, Superadmin")]
        public async Task<IActionResult> DeleteComment(int? commentID) {
            await _commentService.Delete(commentID);
            return Ok();
        }
    }
}
