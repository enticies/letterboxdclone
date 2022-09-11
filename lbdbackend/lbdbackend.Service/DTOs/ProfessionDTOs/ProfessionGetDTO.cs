using FluentValidation;

namespace lbdbackend.Service.DTOs.ProfessionDTOs {
    public class ProfessionGetDTO {
        public string Name { get; set; }
        public int ID;
        public bool IsDeleted { get; set; }
    }

    public class ProfessionGetValidator : AbstractValidator<ProfessionGetDTO> {
        public ProfessionGetValidator() {
            RuleFor(r => r.Name)
                .MaximumLength(25).WithMessage("Maximum length is 25 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
        }
    }
}