using FluentValidation;

namespace lbdbackend.Service.DTOs.ProfessionDTOs {
    public class ProfessionUpdateDTO {
        public int ID {
            get; set;
        }
        public string Name {
            get; set;
        }
    }

    public class ProfessionUpdateValidator : AbstractValidator<ProfessionUpdateDTO> {
        public ProfessionUpdateValidator() {
            RuleFor(r => r.Name)
                .MaximumLength(25).WithMessage("Maximum length is 25 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");

        }

    }
}