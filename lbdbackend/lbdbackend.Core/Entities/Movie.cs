using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class Movie : BaseEntity {
        public string Name { get; set; }
        public int YearID { get; set; }
        public Year Year { get; set; }
        public string Synopsis { get; set; }
        public string BackgroundImage { get; set; }
        public string PosterImage { get; set; }
        public List<JoinMoviesGenres> JoinMoviesGenres { get; set; }
        public List<JoinMoviesPeople> JoinMoviesPeople { get; set; }
    }
}
