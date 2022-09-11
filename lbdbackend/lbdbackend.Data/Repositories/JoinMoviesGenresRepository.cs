using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class JoinMoviesGenresRepository : Repository<JoinMoviesGenres>, IJoinMoviesGenresRepository {
        public JoinMoviesGenresRepository(AppDbContext context) : base(context) {

        }
    }

}
