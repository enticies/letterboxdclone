using FluentValidation;
using lbdbackend.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace lbdbackend.Service.DTOs.MovieDTOs {
    public class MovieUpdateDTO {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public int YearID { get; set; }
        public List<int> Genres { get; set; }
        public List<int> People { get; set; }

        public IFormFile BackgroundImage { get; set; }
        public IFormFile PosterImage { get; set; }
    }

    public class MovieUpdateValidator : AbstractValidator<MovieUpdateDTO> {
        public MovieUpdateValidator() {
            RuleFor(r => r.Name)
                .MaximumLength(40).WithMessage("Maximum length is 40 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
            RuleFor(r => r.Synopsis)
                .MaximumLength(40).WithMessage("Maximum length is 40 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
        }

    }
}
