using ID.UI.Components.Base;
using ID.UI.ViewModel.Claims;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.Claims
{
    public class SetClaimsComponent : IDBaseComponent
    {
        [Parameter] public List<ClaimViewModel> CurrentClaims { get; set; } = new List<ClaimViewModel>();

        protected ClaimViewModel Model { get; set; } = new ClaimViewModel();
        protected MudForm ModelForm { get; set; } = new MudForm();

        protected virtual async Task AddClaimToListAsync()
        {
            await ModelForm.Validate();

            if (ModelForm.IsValid)
            {
                if (CurrentClaims.Any(x => x.Type == Model.Type && x.Value == Model.Value))
                {
                    return;
                }
                else
                {
                    CurrentClaims.Add(new ClaimViewModel()
                    {
                        Value = Model.Value,
                        Type = Model.Type,
                    });

                    Model.Reset();
                }
            }
        }

        protected virtual void RemoveClaimInList(ClaimViewModel model)
        {
            CurrentClaims.Remove(model);
        }
    }
}
