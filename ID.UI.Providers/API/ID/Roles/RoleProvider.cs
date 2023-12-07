using ID.UI.Core;
using ID.UI.Core.Options;
using ID.UI.Core.Roles;
using ID.UI.Core.Roles.Abstractions;
using ID.UI.Core.Roles.Models;
using ID.UI.Core.Users;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http.Json;

namespace ID.UI.Providers.API.ID.Roles
{
    public class RoleProvider : IRoleProvider
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly ApiOptions _apiOptions;

        protected string _accessToken = string.Empty;

        public RoleProvider(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> apiAccessor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiOptions = apiAccessor?.Value ?? new ApiOptions();
        }

        public void WithAccessToken(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<AjaxResult<RoleModel>> CreateAsync(CreateRoleModel role)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.PostAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + "api/role/create", role);

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<RoleModel>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<RoleModel>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<RoleModel>.Error("Не удалось добавить роль");

                return result;
            }
            catch
            {
                return AjaxResult<RoleModel>.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult> DeleteAsync(string roleId)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.DeleteAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/role/{roleId}/remove");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult.Error("Не удалось удалить данные роли");

                return result;
            }
            catch
            {
                return AjaxResult.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult> EditAsync(EditRoleModel role)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.PutAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + "api/role/edit", role);

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult.Error("Не удалось обновить данные роли");

                return result;
            }
            catch
            {
                return AjaxResult.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult<RoleModel>> FindByIdAsync(string roleId)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.GetAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/role/{roleId}");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<RoleModel>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<RoleModel>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<RoleModel>.Error("Не удалось получить данные роли");

                return result;
            }
            catch
            {
                return AjaxResult<RoleModel>.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult<IEnumerable<RoleModel>>> GetAsync(RoleSearchFilter filter)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.GetAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/role?id={filter.Id}&name={filter.Name}");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<IEnumerable<RoleModel>>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<IEnumerable<RoleModel>>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<IEnumerable<RoleModel>>.Error("Не удалось получить данные ролей");

                return result;
            }
            catch
            {
                return AjaxResult<IEnumerable<RoleModel>>.Error("Произошла неизвестная ошибка");
            }
        }
    }
}
