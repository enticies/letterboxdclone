using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace lbdbackend.Service.DTOs.PersonDTOs {
    public class PersonUpdateDTO {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public int ProfessionID { get; set; }

    }

    public class PersonUpdateValidator : AbstractValidator<PersonUpdateDTO> {
        public PersonUpdateValidator() {
            RuleFor(r => r.Name)
                .MaximumLength(25).WithMessage("Maximum length is 25 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
            RuleFor(r => r.Description)
                .MaximumLength(300).WithMessage("Maximum length is 300 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");


        }

    }
}