using FluentValidation;

namespace ID.UI.ViewModel.Clients.Validators
{
    public class EditClientViewModelValdiator : AbstractValidator<EditClientViewModel>
    {
        public EditClientViewModelValdiator()
        {
            RuleFor(x => x.AccessTokenLifetime)
                .Must(x => x > 0).WithMessage("Укажите значение больше 0");
            RuleFor(x => x.AllowedGrantTypes)
                .NotNull().WithMessage("Укажите способ авторизации приложения")
                .NotEmpty().WithMessage("Укажите способ авторизации приложения")
                .Must(x => x.Count > 0).WithMessage("Укажите способ авторизации приложения");
            RuleFor(x => x.AllowedScopes)
                .Must(x => x.Count > 0).WithMessage("Укажите области доступа для приложения");
            RuleFor(x => x.AuthorizationCodeLifetime)
                .Must(x => x > 0).WithMessage("Укажите значение больше 0");
            RuleFor(x => x.Claims)
                .Must(x => x.Count > 0).WithMessage("Укажите хотя бы одно утверждение");
            RuleFor(x => x.ClientName)
                .NotNull().WithMessage("Поле обязательно к заполнению")
                .NotEmpty().WithMessage("Поле обязательно к заполнению");
            RuleFor(x => x.IdentityTokenLifetime)
                .Must(x => x > 0).WithMessage("Укажите значение больше 0");
            RuleFor(x => x.SlidingRefreshTokenLifetime)
                .Must(x => x > 0).WithMessage("Укажите значение больше 0");
        }

        public Func<object, string, Task<IEnumerable<string>>> IsValid => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<EditClientViewModel>
                .CreateWithOptions((EditClientViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return new List<string>() { result.Errors[0].ErrorMessage };
        };
    }
}
