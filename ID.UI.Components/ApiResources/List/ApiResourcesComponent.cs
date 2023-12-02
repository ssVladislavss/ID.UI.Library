﻿using ID.UI.Components.ApiResources.Create;
using ID.UI.Components.Base;
using ID.UI.Core.ApiResources;
using ID.UI.Core.ApiResources.Abstractions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ID.UI.Components.ApiResources.List
{
    public class ApiResourcesComponent : IDBaseComponent
    {
        private List<IDApiResource> _apiResources = new();
        protected IEnumerable<IDApiResource> ApiResources
        {
            get
            {
                foreach (var resource in _apiResources)
                    yield return resource;
            }
        }
        protected HashSet<IDApiResource> SelectedResources = new();
        protected MudTable<IDApiResource> ResourceTable = new MudTable<IDApiResource>();

        [Inject] protected IApiResourceService? ApiResourceService { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            ApiResourceService!.OnTokenError += OnTokenError;
            ApiResourceService!.OnGetToken += OnGetToken;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await GetResourcesAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected virtual async Task GetResourcesAsync()
        {
            OverlayEnabled = true;

            StateHasChanged();

            var sendResult = await ApiResourceService!.GetAsync(new ApiResourceSearchFilter());
            if(sendResult.Result == Core.AjaxResultTypes.Success && sendResult.Data != null)
            {
                _apiResources = sendResult.Data.ToList();
            }

            OverlayEnabled = false;

            StateHasChanged();
        }

        protected virtual async Task RemoveResourcesAsync()
        {
            if(SelectedResources.Count > 0)
            {
                OverlayEnabled = true;

                StateHasChanged();

                foreach (var resource in SelectedResources)
                {
                    var removeResult = await ApiResourceService!.RemoveAsync(resource.Id);
                    if(removeResult.Result != Core.AjaxResultTypes.Success)
                    {
                        Snackbar?.Add($"{resource.Name} - {removeResult.Message}", MudBlazor.Severity.Error);
                    }
                    else
                    {
                        _apiResources.Remove(resource);
                        Snackbar?.Add($"{resource.Name} - удалена", MudBlazor.Severity.Success);
                    }
                }

                OverlayEnabled = false;

                StateHasChanged();
            }
        }

        protected virtual async Task ShowCreateResourceDialogAsync()
        {
            var dialogReference = await DialogService!.ShowAsync<CreateApiResourceDialog>("", new DialogOptions
            {
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            });

            var dialogResult = await dialogReference.Result;
            if(dialogResult != null)
                if(dialogResult.Data != null && dialogResult.Data is IDApiResource apiResource)
                    _apiResources.Add(apiResource);
        }

        protected virtual async Task EditResourceStatusAsync(IDApiResource apiResource)
        {
            OverlayEnabled = true;

            StateHasChanged();

            var editResult = await ApiResourceService!.EditStatusAsync(new Core.ApiResources.Models.EditApiResourceStatusModel
            {
                Id = apiResource.Id,
                Status = !apiResource.Enabled
            });

            if(editResult.Result == Core.AjaxResultTypes.Success)
            {
                apiResource.Enabled = !apiResource.Enabled;
                Snackbar?.Add("Статус ресурса изменен", Severity.Success);
            }
            else
            {
                Snackbar?.Add(editResult.Message, Severity.Error);
            }

            OverlayEnabled = false;

            StateHasChanged();
        }
    }
}
