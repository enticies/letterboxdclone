using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

    }
}
