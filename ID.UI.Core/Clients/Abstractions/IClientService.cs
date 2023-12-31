﻿using ID.UI.Core.Clients.Models;
using IdentityServer4.Models;

namespace ID.UI.Core.Clients.Abstractions
{
    public interface IClientService
    {
        Task<AjaxResult<IEnumerable<Client>>> GetAsync(ClientSearchFilter filter);
        Task<AjaxResult<Client>> FindAsync(string clientId);
        Task<AjaxResult<Client>> CreateAsync(CreateClientModel data);
        Task<AjaxResult> EditAsync(EditClientModel data);
        Task<AjaxResult> EditStatusAsync(EditClientStatusModel data);
        Task<AjaxResult> RemoveAsync(string clientId);
    }
}
