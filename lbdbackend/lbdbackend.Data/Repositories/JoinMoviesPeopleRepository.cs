using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class JoinMoviesPeopleRepository : Repository<JoinMoviesPeople>, IJoinMoviesPeopleRepository {
        public JoinMoviesPeopleRepository(AppDbContext context) : base(context) {

        }
    }

}
