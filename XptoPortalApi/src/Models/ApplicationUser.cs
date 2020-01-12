using System;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace XptoPortalApi.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public ValidationResult Validate()
        {
            var validationResult = new ApplicationUserValidation().Validate(this);
            return validationResult;
        }
    }

    public class ApplicationUserValidation : AbstractValidator<ApplicationUser>
    {
        public ApplicationUserValidation()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("Please specify a UserName");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Please specify a Email");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Please specify a Name");
        }
        private bool BeAValidDate(DateTime updateDate)
        {
            return updateDate != default(DateTime);
        }
    }
}
