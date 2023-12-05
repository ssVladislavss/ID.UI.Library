using FluentValidation;

namespace ID.UI.ViewModel.Users.Validators
{
    public class CreateUserViewModelValidator : AbstractValidator<CreateUserViewModel>
    {
        public CreateUserViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("Заполните поле")
                .NotEmpty().WithMessage("Заполните поле")
                .EmailAddress().WithMessage("Пример: example@test.ru");

            RuleFor(x => x.RoleNames)
                .Must(x => x.Count > 0).WithMessage("Укажите хотя бы одну роль");
        }

        public Func<object, string, Task<IEnumerable<string>>> IsValid => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateUserViewModel>
                .CreateWithOptions((CreateUserViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return new List<string>() { result.Errors[0].ErrorMessage };
        };
    }
}
