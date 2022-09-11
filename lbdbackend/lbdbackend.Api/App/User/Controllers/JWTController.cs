using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lbdbackend.Api.App.User.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase {
        private readonly IJWTManager _jwtManager;

        public JWTController(IJWTManager jwtManager) {
            _jwtManager = jwtManager;
        }


    }
}
