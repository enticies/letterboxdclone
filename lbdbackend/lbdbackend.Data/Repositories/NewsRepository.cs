using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class NewsRepository : Repository<News>, INewsRepository {
        public NewsRepository(AppDbContext context) : base(context) {

        }
    }

}
