using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace lbdbackend.Service.DTOs.PersonDTOs {
    public class PersonGetDTO {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int ProfessionID { get; set; }
        public string ProfessionName { get; set; }


        public int ID;
        public bool IsDeleted { get; set; }
    }

    public class PersonGetValidator : AbstractValidator<PersonGetDTO> {
        public PersonGetValidator() {
            RuleFor(r => r.Name)
                .MaximumLength(25).WithMessage("Maximum length is 25 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
            RuleFor(r => r.Description)
                .MaximumLength(300).WithMessage("Maximum length is 300 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
        }
    }
}