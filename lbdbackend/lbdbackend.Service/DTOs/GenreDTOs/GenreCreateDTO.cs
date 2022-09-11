using FluentValidation;

namespace lbdbackend.Service.DTOs.GenreDTOs {
    public class GenreCreateDTO {
        public string Name {
            get; set;
        }
    }

    public class GenreCreateValidator : AbstractValidator<GenreCreateDTO> {
        public GenreCreateValidator() {
            RuleFor(r => r.Name)
                .MaximumLength(25).WithMessage("Maximum length is 25 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
        }
    }
}