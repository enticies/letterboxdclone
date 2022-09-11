using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lbdbackend.Api.App.User.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase {
        private readonly IUserService _userService;
        public MembersController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var recentMembers = await _userService.GetRecentMembers();
            return Ok(new {
                recentMembers = recentMembers,
            });
        }

        [HttpGet]
        [Route("getpaginatedusers")]
        public async Task<IActionResult> GetPaginatedUser(int i = 1) {
            var members = await _userService.GetPaginatedUsers(i);
            return Ok(new { members = members });
        }
    }
}
