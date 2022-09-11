using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class ReviewRepository : Repository<Review>, IReviewRepository {
        public ReviewRepository(AppDbContext context) : base(context) {

        }
    }

}
