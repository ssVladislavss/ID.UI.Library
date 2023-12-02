using FluentValidation;

namespace ID.UI.ViewModel.Secrets.Validators
{
    public class SecretViewModelValidator : AbstractValidator<SecretViewModel>
    {
        public SecretViewModelValidator()
        {
            RuleFor(x => x.Value)
                .NotNull().WithMessage("Заполните поле")
                .NotEmpty().WithMessage("Заполните поле");
        }

        public Func<object, string, Task<IEnumerable<string>>> IsValid => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<SecretViewModel>
                .CreateWithOptions((SecretViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return new List<string>() { result.Errors[0].ErrorMessage };
        };
    }
}
