using lbdbackend.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Interfaces {
    public interface IJWTManager {

        Task<string> GenerateToken(AppUser user);
        string GetUsernameByToken(string token);
       string decodeJWT(string token);
    }
}
