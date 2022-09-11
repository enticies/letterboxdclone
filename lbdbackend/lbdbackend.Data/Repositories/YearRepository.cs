using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class YearRepository : Repository<Year>, IYearRepository {
        public YearRepository(AppDbContext context) : base(context) {

        }
    }

}
