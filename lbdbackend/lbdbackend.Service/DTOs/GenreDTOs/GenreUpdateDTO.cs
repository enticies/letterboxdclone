using FluentValidation;

namespace lbdbackend.Service.DTOs.GenreDTOs {
    public class GenreUpdateDTO {
        public int ID {
            get; set; 
        }
        public string Name {
            get; set;
        }
    }

    public class GenrePostValidator : AbstractValidator<GenreUpdateDTO> {
        public GenrePostValidator() {
            RuleFor(r => r.Name)
                .MaximumLength(25).WithMessage("Maximum length is 25 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");

        }

    }
}
