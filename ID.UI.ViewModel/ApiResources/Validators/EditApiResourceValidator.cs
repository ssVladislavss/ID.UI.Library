using FluentValidation;

namespace ID.UI.ViewModel.ApiResources.Validators
{
    public class EditApiResourceValidator : AbstractValidator<EditApiResourceViewModel>
    {
        public EditApiResourceValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Заполните поле")
                .NotEmpty().WithMessage("Заполните поле");
            RuleFor(x => x.ApiSecrets)
                .Must(secrets => secrets.Count >= 1).WithMessage("Необходим хотя бы один ключ");
        }

        public Func<object, string, Task<IEnumerable<string>>> IsValid => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<EditApiResourceViewModel>
                .CreateWithOptions((EditApiResourceViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return new List<string>() { result.Errors[0].ErrorMessage };
        };
    }
}
