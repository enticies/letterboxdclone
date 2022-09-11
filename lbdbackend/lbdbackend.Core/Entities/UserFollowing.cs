using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class UserFollowing : BaseEntity {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string FolloweeId { get; set; }
        public AppUser Followee { get; set; }
    }
}
