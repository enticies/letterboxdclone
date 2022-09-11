using FluentValidation;
using lbdbackend.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace lbdbackend.Service.DTOs.MovieDTOs {
    public class MovieCreateDTO {
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public int YearID { get; set; }
        public List<int> Genres { get; set; }
        public List<int> People { get; set; }

        public IFormFile BackgroundImage { get; set; }
        public IFormFile PosterImage { get; set; }
    }

    public class MovieCreateValidator : AbstractValidator<MovieCreateDTO> {
        public MovieCreateValidator() {
            RuleFor(r => r.Name)
                .MaximumLength(100).WithMessage("Maximum length is 100 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
            RuleFor(r => r.Synopsis)
                .MaximumLength(300).WithMessage("Maximum length is 300 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
        }

    }
}
