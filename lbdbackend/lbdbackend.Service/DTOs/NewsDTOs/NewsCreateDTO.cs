using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.DTOs.NewsDTOs {
    public class NewsCreateDTO {
        public string Title { get; set; }
        public string Body { get; set; }
        public string OwnerUserName { get; set; }
        public IFormFile Image { get; set; }
    }

    public class NewsCreateValidator : AbstractValidator<NewsCreateDTO> {
        public NewsCreateValidator() {
            RuleFor(r => r.Title)
                .MaximumLength(100).WithMessage("Maximum length is 100 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
            RuleFor(r => r.Body)
                .MaximumLength(3000).WithMessage("Maximum length is 3000 symbols.")
                .NotEmpty().WithMessage("Cannot be empty.");
            RuleFor(r => r.OwnerUserName)
                .NotEmpty().WithMessage("Cannot be empty.");
        }
    }

}
