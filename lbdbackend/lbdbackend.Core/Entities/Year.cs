using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities
{
    public class Year : BaseEntity
    {
        public int YearNumber { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
