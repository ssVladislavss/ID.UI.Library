using FluentValidation;

namespace ID.UI.ViewModel.Clients.Validators
{
    public class ClientClaimViewModelValidator : AbstractValidator<ClientClaimViewModel>
    {
        public ClientClaimViewModelValidator()
        {
            RuleFor(x => x.Type)
                .NotNull().WithMessage("Укажите тип утверждения")
                .NotEmpty().WithMessage("Поле не может быть пустым");
            RuleFor(x => x.Value)
                .NotNull().WithMessage("Укажите значение утверждения")
                .NotEmpty().WithMessage("Поле не может быть пустым");
        }

        public Func<object, string, Task<IEnumerable<string>>> IsValid => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ClientClaimViewModel>
                .CreateWithOptions((ClientClaimViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return new List<string>() { result.Errors[0].ErrorMessage };
        };
    }
}
