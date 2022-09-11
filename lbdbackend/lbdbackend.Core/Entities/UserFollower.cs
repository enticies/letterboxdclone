using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class UserFollower : BaseEntity {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string FollowerId { get; set; }
        public AppUser Follower { get; set; }

    }
}
