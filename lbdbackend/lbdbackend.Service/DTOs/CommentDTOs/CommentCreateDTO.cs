using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.DTOs.CommentDTOs {
    public class CommentCreateDTO {
        public string OwnerId { get; set; }
        public string Body { get; set; }
        public int ReviewID { get; set; }
    }
    public class CommentCreateValidator : AbstractValidator<CommentCreateDTO> {
        public CommentCreateValidator() {
            RuleFor(r => r.Body).MaximumLength(300).WithMessage("Maximum length is 300 symbols.");
            RuleFor(r => r.OwnerId).NotNull().WithMessage("AppUserId cannot be null.");
            RuleFor(r =>  r.ReviewID).NotNull().WithMessage("ReviewId cannot be null.");
        }
    }

}
