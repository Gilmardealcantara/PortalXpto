using System;
using FluentValidation;
using FluentValidation.Results;

namespace XptoPortalApi.Models
{

    public class Auth : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public override ValidationResult Validate()
        {
            var validationResult = new AuthValidation().Validate(this);
            return validationResult;
        }
    }

    public class AuthValidation : AbstractValidator<Auth>
    {
        public AuthValidation()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Please specify a UserName");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Please specify a Password");
        }
        private bool BeAValidDate(DateTime updateDate)
        {
            return updateDate != default(DateTime);
        }
    }
}
