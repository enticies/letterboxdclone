using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.DTOs.NewsDTOs {
    public class NewsGetDTO {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Body { get; set; }
        public string OwnerUsername { get; set; }
        public int Id { get; set; } 
    }
}
