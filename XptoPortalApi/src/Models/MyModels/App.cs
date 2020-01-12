using System;
using FluentValidation;
using FluentValidation.Results;

namespace XptoPortalApi.Models
{

    public class App : BaseEntity
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public override ValidationResult Validate()
        {
            var validationResult = new AppValidation().Validate(this);
            return validationResult;
        }
    }

    public class AppValidation : AbstractValidator<App>
    {
        public AppValidation()
        {
            RuleFor(x => x.Url).NotNull().NotEmpty().WithMessage("Please specify a Url");
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Please specify a Title");
        }
        private bool BeAValidDate(DateTime updateDate)
        {
            return updateDate != default(DateTime);
        }
    }
}
