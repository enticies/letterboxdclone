using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class JoinMoviesPeople : BaseEntity {
        public int MovieID { get; set; }
        public Movie Movie { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; }
    }
}
