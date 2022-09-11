using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.DTOs.ListDTOs {
    public class MovieListGetDTO {
        public string Name { get; set; }
        public string OwnerUsername { get; set; }
        public int MovieCount { get; set; }
        public int Id { get; set; }
    }
}
