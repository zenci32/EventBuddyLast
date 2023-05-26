using Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.CategoryRepository.Validation
{
    internal class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        { 
            RuleFor(p=>p.CategoryName).NotEmpty().WithMessage("Boş Geçilemez");
            RuleFor(p => p.CategoryName).MaximumLength(50).WithMessage("50 Karakterden Fazla Olamaz");
        }
    }
}
