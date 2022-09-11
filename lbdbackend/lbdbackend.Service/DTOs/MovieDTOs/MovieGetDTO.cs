using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace lbdbackend.Service.DTOs.MovieDTOs {
    public class MovieGetDTO {
        public string Name { get; set; }
        public string Synopsis { get; set; }
        //public int YearID { get; set; }
        public int YearNumber { get; set; } 
        public string BackgroundImage { get; set; }

        public string PosterImage { get; set; }
        public bool IsDeleted { get; set; }
        public int ID { get; set; }
    }

    public class MovieGetValidator : AbstractValidator<MovieGetDTO> {
        public MovieGetValidator() {
            RuleFor(r => r.Name)
                .MaximumLength(25).WithMessage("Maximum length is 25 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
            RuleFor(r => r.Synopsis)
                .MaximumLength(300).WithMessage("Maximum length is 300 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
        }
    }
}