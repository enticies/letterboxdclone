using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class JoinMoviesLists : BaseEntity {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int MovieListId { get; set; }
        public MovieList MovieList { get; set; }
    }
}
