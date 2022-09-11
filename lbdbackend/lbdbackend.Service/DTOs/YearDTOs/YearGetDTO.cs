using FluentValidation;

namespace lbdbackend.Service.DTOs.YearDTOs {
    public class YearGetDTO {
        public int ID { get; set; } 
        public string YearNumber { get; set; }
    }
}