using AutoMapper;
using lbdbackend.Core.Entities;
using lbdbackend.Service.DTOs.AccountDTOs;
using lbdbackend.Service.DTOs.UserDTOs;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace lbdbackend.Api.App.User.Controllers {
    [Route("api/[controller]")]
    [ApiController]


    public class AccountsController : ControllerBase {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jwtManager;
        private readonly IEmailService _emailService;


        public AccountsController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IJWTManager jwtManager, IUserService userService, IEmailService emailService) {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtManager = jwtManager;
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO) {
            AppUser user = _mapper.Map<AppUser>(registerDTO);
            user.Image = "defaultuser.jpg";

            IdentityResult identityResult = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!identityResult.Succeeded) {
                return BadRequest(identityResult.Errors);
            }

            identityResult = await _userManager.AddToRoleAsync(user, "Member");
            AppUser appUser = await _userManager.FindByEmailAsync(registerDTO.Email);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var link = Url.Action("ConfirmEmail", "Accounts", new { userId = appUser.Id, token = code }, Request.Scheme, Request.Host.ToString());

            _emailService.Register(registerDTO, link);

            return StatusCode(200);
        }

        [HttpGet]
        [Route("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token) {
            AppUser user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, token);
            return Ok();

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO) {
            AppUser foundByEmail = await _userManager.FindByEmailAsync(loginDTO.EmailOrUsername);
            AppUser foundByUserName = await _userManager.FindByNameAsync(loginDTO.EmailOrUsername);
         
            if (foundByEmail != null && await _userManager.CheckPasswordAsync(foundByEmail, loginDTO.Password) && foundByEmail.EmailConfirmed) {
                var token = await _jwtManager.GenerateToken(foundByEmail);
                return Ok(new {
                    userData = new {
                        username = foundByEmail.UserName,
                        email = foundByEmail.Email,
                        id = foundByEmail.Id,
                        token = token,
                        image = foundByEmail.Image,
                    }
                });
            }
            else if (foundByUserName != null && await _userManager.CheckPasswordAsync(foundByUserName, loginDTO.Password) && foundByUserName.EmailConfirmed) {
                var token = await _jwtManager.GenerateToken(foundByUserName);
                return Ok(new {
                    userData = new {
                        username = foundByUserName.UserName,
                        email = foundByUserName.Email,
                        id = foundByUserName.Id,
                        token = token,
                        image = foundByUserName.Image,
                    }
                })
                ;
            }

            return NotFound("Your credentials don’t match. It’s probably attributable to human error.");
        }

        [HttpGet]
        [Route("CheckToken")]
        [Authorize(Roles = "Superadmin, Admin, Member")]

        public async Task<IActionResult> CheckToken() {
            return Ok();
        }

        [HttpGet]
        [Route("getuser")]
        public async Task<IActionResult> GetUser(string userName) {
            return Ok(await _userService.GetUserMain(userName));
        }

        [HttpPost]
        [Route("follow")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Follow(string followerUsername, string followeeUsername) {
            var isFollowing = await _userService.Follow(followerUsername, followeeUsername);
            return Ok(isFollowing);
        }
        [HttpGet]
        [Route("checkfollow")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> CheckFollow(string followerUsername, string followeeUsername) {
            if (await _userService.CheckFollow(followerUsername, followeeUsername)) {
                return Ok("true");
            }
            return Ok("false");
        }

        [HttpGet]
        [Route("getuserfollowers")]
        public async Task<IActionResult> GetUserFollowers(string userName, int i = 1) {
            return Ok(await _userService.GetUserFollowers(userName, i));
        }

        [HttpGet]
        [Route("getuserfollowees")]
        public async Task<IActionResult> GetUserFollowees(string userName, int i = 1) {
            return Ok(await _userService.GetUserFollowees(userName, i));
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        [Route("changeuseravatar")]

        public async Task<IActionResult> ChangeUserAvatar([FromForm] UserImageDTO userImageDTO) {
            await _userService.ChangeUserImage(userImageDTO);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        [Route("changeusercredentials")]

        public async Task<IActionResult> ChangeUserCredentials(string userName, UserChangeDTO userChangeDTO) {
            await _userService.ChangeUserCredentials(userName, userChangeDTO);
            return Ok();
        }

        [HttpGet]
        [Route("getusersbystring")]
        public async Task<IActionResult> GetUserByString(string s, int i = 1) {
            var users = await _userService.GetPaginatedUsers(s, i);
            return Ok(users);
        }




        //[HttpGet]
        //public async Task<IActionResult> CreateRoles() {
        //    //initializing custom roles 
        //    string[] roleNames = { "Superadmin", "Admin", "Member" };
        //    IdentityResult roleResult;

        //    foreach (var roleName in roleNames) {
        //        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        //        if (!roleExist) {
        //            //create the roles and seed them to the database: Question 1
        //            roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
        //        }
        //    }

        //    return Content("Done.");
        //}

        //[HttpGet]
        //public async Task<IActionResult> createsuperadmin() {
        //    //AppUser superAdmin = new AppUser();

        //    //superAdmin.Email = "lasauthr@protonmail.com";
        //    ////superAdmin.UserName = "Superadmin";
        //    //await _userManager.CreateAsync(superAdmin, "Supp123!");
        //    await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync("lasauthr@protonmail.com"), "Superadmin");

        //    return Content("Password changed.");

        //}

    }
}
