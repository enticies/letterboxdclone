using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.DTOs.AccountDTOs {
    public class RegisterDTO {
        public string Email {
            get; set;
        }

        public string Username {
            get; set;
        }
        public string Password {
            get; set;
        }
        public string ConfirmPassword {
            get; set;
        }

    }
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO> {
        public RegisterDTOValidator() {
            RuleFor(r => r.Email)
                .EmailAddress().WithMessage("Email is incorrect");

            RuleFor(r => r.Username)
                .MinimumLength(6).WithMessage("Minimum username length is 6.");

            RuleFor(r => r.Password)
                .MinimumLength(8).WithMessage("Password minimum length is 8 symbols.");

            RuleFor(r => r.ConfirmPassword)
                .MinimumLength(8).WithMessage("Password minimum length is 8 symbols.")
                .Equal(e => e.Password).WithMessage("Passwords do not match.");
        }
    }
}
