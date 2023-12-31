﻿using ID.UI.Core.ApiResources.Models;

namespace ID.UI.Core.ApiResources.Abstractions
{
    public interface IApiResorceProvider
    {
        void WithAccessToken(string accessToken);
        Task<AjaxResult<IEnumerable<IDApiResource>>> GetAsync(ApiResourceSearchFilter filter);
        Task<AjaxResult<IDApiResource>> FindAsync(int resourceId);
        Task<AjaxResult<IDApiResource>> CreateAsync(CreateApiResourceModel data);
        Task<AjaxResult> EditAsync(EditApiResourceModel data);
        Task<AjaxResult> EditStatusAsync(EditApiResourceStatusModel data);
        Task<AjaxResult> RemoveAsync(int resourceId);
    }
}
