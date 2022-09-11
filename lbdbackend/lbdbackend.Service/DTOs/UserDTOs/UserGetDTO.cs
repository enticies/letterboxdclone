using lbdbackend.Service.DTOs.ReviewDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.DTOs.UserDTOs {
    public class UserGetDTO {
        public int FilmCount { get; set; }
        public int ListCount { get; set; }
        public int FollowerCount { get; set; }
        public int FolloweeCount { get; set; }
        public int ReviewCount { get; set; }
        public int MovieListCount { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public List<ReviewGetDTO> RecentReviews { get; set; }

    }
}
