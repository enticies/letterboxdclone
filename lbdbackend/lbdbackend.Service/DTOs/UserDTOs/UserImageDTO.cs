using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.DTOs.UserDTOs {
    public class UserImageDTO {
        public string UserName { get; set; }
        public IFormFile Image { get; set; }
    }
}
