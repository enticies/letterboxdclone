using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class News : BaseEntity {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
    }
}
