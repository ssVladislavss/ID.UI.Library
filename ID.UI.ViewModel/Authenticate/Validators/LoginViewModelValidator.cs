using FluentValidation;

namespace ID.UI.ViewModel.Authenticate.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("Укажите email адрес")
                .NotEmpty().WithMessage("Поле не может быть пустым")
                .EmailAddress().WithMessage("Пример: example@test.ru");
            RuleFor(x => x.Password)
                .NotNull().WithMessage("Заполните поле")
                .NotEmpty().WithMessage("Заполните поле")
                .MinimumLength(4).WithMessage("Минимальная длина 4 символа");
        }

        public Func<object, string, Task<IEnumerable<string>>> IsValid => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<LoginViewModel>
                .CreateWithOptions((LoginViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return new List<string>() { result.Errors[0].ErrorMessage };
        };
    }
}
