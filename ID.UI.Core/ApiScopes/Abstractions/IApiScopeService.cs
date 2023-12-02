﻿using ID.UI.Core.ApiScopes.Models;

namespace ID.UI.Core.ApiScopes.Abstractions
{
    public interface IApiScopeService
    {
        delegate Task<string?> GetTokenHandler();
        delegate Task TokenErrorHandler();
        event GetTokenHandler? OnGetToken;
        event TokenErrorHandler? OnTokenError;

        Task<AjaxResult<IEnumerable<IDApiScope>>> GetAsync(ApiScopeSearchFilter filter);
        Task<AjaxResult<IDApiScope>> FindAsync(int scopeId);
        Task<AjaxResult<IDApiScope>> CreateAsync(CreateApiScopeModel data);
        Task<AjaxResult> EditAsync(EditApiScopeModel data);
        Task<AjaxResult> EditStatusAsync(EditApiScopeStatusModel data);
        Task<AjaxResult> RemoveAsync(int scopeId);
    }
}
