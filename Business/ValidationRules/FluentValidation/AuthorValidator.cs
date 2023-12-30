using DataAccess.Entities;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(a=>a.Name).NotEmpty().MaximumLength(50);
        RuleFor(a=>a.Email).NotEmpty().MaximumLength(50).EmailAddress();
    }
}