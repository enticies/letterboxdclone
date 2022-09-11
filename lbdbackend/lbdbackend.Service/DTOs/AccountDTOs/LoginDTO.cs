using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.DTOs.AccountDTOs {
    public class LoginDTO {
        public string EmailOrUsername {
            get; set; 
        }
        public string Password {
            get; set;
        }
    }

    public class LoginDTOValidator : AbstractValidator<LoginDTO> {
        public LoginDTOValidator() {
            RuleFor(r => r.Password)
                .MinimumLength(8).WithMessage("Password minimum length is 6 symbols.");

            RuleFor(r => r.EmailOrUsername)
                .NotEmpty().WithMessage("Email or Username is required.");
        }
    }
}
