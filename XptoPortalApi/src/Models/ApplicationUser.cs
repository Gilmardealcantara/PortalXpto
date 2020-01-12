using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace XptoPortalApi.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Token { get; private set; }
        public ValidationResult Validate()
        {
            var validationResult = new ApplicationUserValidation().Validate(this);
            return validationResult;
        }

        public void GenerateToken(string secret, string issuer, string audience)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secret));

            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha512);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, this.Name),
                new Claim(ClaimTypes.NameIdentifier, this.UserName),
                new Claim(ClaimTypes.Email, this.Email),
            };

            var handler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = handler.CreateJwtSecurityToken(
                issuer,
                audience,
                new ClaimsIdentity(claims),
                DateTime.Now,
                DateTime.Now.AddDays(10),
                DateTime.Now,
                signingCredentials);

            this.Token = handler.WriteToken(jwtSecurityToken);
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
