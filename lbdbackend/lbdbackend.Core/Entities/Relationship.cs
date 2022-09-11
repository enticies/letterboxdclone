using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class Relationship : BaseEntity {
        public string FollowerId { get; set; }
        public AppUser Follower { get; set; }
        public string FolloweeId { get; set; }
        public AppUser Followee { get; set; }
    }
}
