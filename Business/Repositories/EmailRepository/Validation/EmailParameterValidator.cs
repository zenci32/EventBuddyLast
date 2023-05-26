using Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.EmailRepository.Validation
{
    internal class EmailParameterValidator : AbstractValidator<Email>
    {
        public EmailParameterValidator()
        {
            RuleFor(p => p.Smtp).NotEmpty().WithMessage("SMTP adresi boş olamaz");
            RuleFor(p => p.Mail).NotEmpty().WithMessage("Mail adresi boş olamaz");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre adresi boş olamaz");
            RuleFor(p => p.Port).NotEmpty().WithMessage("Port adresi boş olamaz");
        }
    }
}
