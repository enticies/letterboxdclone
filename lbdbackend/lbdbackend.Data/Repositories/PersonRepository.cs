using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class PersonRepository : Repository<Person>, IPersonRepository {
        public PersonRepository(AppDbContext context) : base(context) {

        }
    }

}
