﻿using FluentValidation;

namespace ID.UI.ViewModel.ApiScopes.Validators
{
    public class EditApiScopeViewModelValidator : AbstractValidator<EditApiScopeViewModel>
    {
        public EditApiScopeViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Укажите наименование")
                .NotEmpty().WithMessage("Поле не может быть пустым");
        }

        public Func<object, string, Task<IEnumerable<string>>> IsValid => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<EditApiScopeViewModel>
                .CreateWithOptions((EditApiScopeViewModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return new List<string>() { result.Errors[0].ErrorMessage };
        };
    }
}
