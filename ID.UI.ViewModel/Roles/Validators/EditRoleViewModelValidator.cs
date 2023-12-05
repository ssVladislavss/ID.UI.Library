using FluentValidation;

namespace ID.UI.ViewModel.Roles.Validators
{
    public class EditRoleViewModelValidator : AbstractValidator<EditRoleViewModel>
    {
        public EditRoleViewModelValidator()
        {
            RuleFor(x => x.RoleName)
                .NotNull().WithMessage("Заполните поле")
                .NotEmpty().WithMessage("Заполните поле");
        }

        public Func<object, string, Task<IEnumerable<string>>> IsValid => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<EditRoleViewModel>
                .CreateWithOptions((EditRoleViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return new List<string>() { result.Errors[0].ErrorMessage };
        };
    }
}
