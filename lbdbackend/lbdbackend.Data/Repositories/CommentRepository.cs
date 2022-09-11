using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class CommentRepository : Repository<Comment>, ICommentRepository {
        public CommentRepository(AppDbContext context) : base(context) {

        }
    }

}

