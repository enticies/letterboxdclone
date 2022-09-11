using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class AppUser : IdentityUser {
        public string Image { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Relationship> Followers { get; set; }
        public List<Relationship> Followings { get; set; }
    }
}
