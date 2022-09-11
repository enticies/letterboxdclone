using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.DTOs.ListDTOs {
    public class MovieListCreateDTO {
        public string Name { get; set; }
        public string OwnerUsername { get; set; }
        public List<int> Movies { get; set; }
    }
}
