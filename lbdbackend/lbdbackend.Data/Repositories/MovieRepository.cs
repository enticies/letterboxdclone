using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class MovieRepository : Repository<Movie>, IMovieRepository {
        public MovieRepository(AppDbContext context) : base(context) {

        }
    }

}
