using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class Comment : BaseEntity {
        public string Body { get; set; }
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
