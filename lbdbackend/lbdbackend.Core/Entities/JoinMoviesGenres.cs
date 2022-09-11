using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class JoinMoviesGenres: BaseEntity {
        public int MovieID{ get; set; }
        public Movie Movie { get; set; }
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
    }
}
