using FluentValidation;

namespace ID.UI.ViewModel.Users.Validators
{
    public class EditUserViewModelValidator : AbstractValidator<EditUserViewModel>
    {
        public EditUserViewModelValidator()
        {
        }

        public Func<object, string, Task<IEnumerable<string>>> IsValid => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<EditUserViewModel>
                .CreateWithOptions((EditUserViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return new List<string>() { result.Errors[0].ErrorMessage };
        };
    }
}
