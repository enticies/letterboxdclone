using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Core.Entities {
    public class Genre : BaseEntity {
        public string Name { get; set; }
        public List<JoinMoviesGenres> JoinMoviesGenres { get; set; }
    }
}
