using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class MovieListRepository : Repository<MovieList>, IMovieListRepository {
        public MovieListRepository(AppDbContext context) : base(context) {

        }
    }

}