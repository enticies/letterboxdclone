using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class Person : BaseEntity {
        public string Name { get; set; }
        public string Image { get; set; }
        public int ProfessionID { get; set; }
        public Profession Profession { get; set; }
        public string Description { get; set; }
        public List<JoinMoviesPeople> JoinMoviesPeople { get; set; }
    }
}