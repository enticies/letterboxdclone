using FluentValidation;

namespace lbdbackend.Service.DTOs.ProfessionDTOs {
    public class ProfessionCreateDTO {
        public string Name {
            get; set;
        }
    }

    public class ProfessionCreateValidator : AbstractValidator<ProfessionCreateDTO> {
        public ProfessionCreateValidator() {
            RuleFor(r => r.Name)
                .MaximumLength(25).WithMessage("Maximum length is 25 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
        }
    }
}