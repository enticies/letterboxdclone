using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Repositories {
    public class RelationshipRepository : Repository<Relationship>, IRelationshipRepository {
        public RelationshipRepository(AppDbContext context) : base(context) {

        }
    }

}
