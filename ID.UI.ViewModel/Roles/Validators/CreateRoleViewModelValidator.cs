using FluentValidation;

namespace ID.UI.ViewModel.Roles.Validators
{
    public class CreateRoleViewModelValidator : AbstractValidator<CreateRoleViewModel>
    {
        public CreateRoleViewModelValidator()
        {
            RuleFor(x => x.RoleName)
                .NotNull().WithMessage("Заполните поле")
                .NotEmpty().WithMessage("Заполните поле");
        }

        public Func<object, string, Task<IEnumerable<string>>> IsValid => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateRoleViewModel>
                .CreateWithOptions((CreateRoleViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return new List<string>() { result.Errors[0].ErrorMessage };
        };
    }
}
