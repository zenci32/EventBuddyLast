using Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.UserRepository.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail adresi boş olamaz");
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Telefon boş olamaz");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçerli bir mail adresi yazın");
            RuleFor(p => p.BirthDay).NotEmpty().WithMessage("Doğum tarihi boş bırakılamaz");
        }
    }
}
