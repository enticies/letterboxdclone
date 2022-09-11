using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class Profession : BaseEntity {
        public string Name { get; set; }

        public List<Person> People { get; set; }
    }
}