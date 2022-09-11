using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public  class MovieList : BaseEntity {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public int MovieCount { get; set; }
        public ICollection<JoinMoviesLists> JoinMoviesLists { get; set; } 
    }
}
