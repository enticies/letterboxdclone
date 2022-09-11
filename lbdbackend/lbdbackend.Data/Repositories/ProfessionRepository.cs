using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class ProfessionRepository : Repository<Profession>, IProfessionRepository {
        public ProfessionRepository(AppDbContext context) : base(context) {

        }
    }

}