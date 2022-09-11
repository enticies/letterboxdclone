using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class GenreRepository : Repository<Genre>, IGenreRepository {
        public GenreRepository(AppDbContext context) : base(context) {

        }
    }

}
