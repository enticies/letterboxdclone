using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.DTOs.CommentDTOs {
    public class CommentGetDTO {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Body { get; set; }
    }
}
