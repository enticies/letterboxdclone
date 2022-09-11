using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class JoinMoviesListsRepository : Repository<JoinMoviesLists>, IJoinMoviesListsRepository {
        public JoinMoviesListsRepository(AppDbContext context) : base(context) {

        }
    }

}
