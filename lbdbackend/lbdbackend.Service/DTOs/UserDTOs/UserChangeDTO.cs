using lbdbackend.Service.DTOs.ReviewDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.DTOs.UserDTOs {
    public class UserChangeDTO {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}