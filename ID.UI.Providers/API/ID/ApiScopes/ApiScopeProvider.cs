using ID.UI.Core;
using ID.UI.Core.ApiScopes;
using ID.UI.Core.ApiScopes.Abstractions;
using ID.UI.Core.ApiScopes.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ID.UI.Providers.API.ID.ApiScopes
{
    public class ApiScopeProvider : IApiScopeProvider
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly ApiOptions _apiOptions;

        protected string _accessTolen = string.Empty;

        public ApiScopeProvider
            (IHttpClientFactory httpClientFactory,
             IOptions<ApiOptions> apiDescriptor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiOptions = apiDescriptor?.Value ?? new ApiOptions();
        }

        public void WithAccessToken(string accessToken)
        {
            this._accessTolen = accessToken;
        }

        public async Task<AjaxResult<IDApiScope>> CreateAsync(CreateApiScopeModel data)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(_accessTolen);

            try
            {
                var sendResult = await client.PostAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/apiscope/create", data);

                if(sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<IDApiScope>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<IDApiScope>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<IDApiScope>.Error("Не удалось добавить область");

                return result;
            }
            catch
            {
                return AjaxResult<IDApiScope>.Error($"Произошла внутренняя ошибка");
            }
        }

        public async Task<AjaxResult> EditAsync(EditApiScopeViewModel data)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(_accessTolen);

            try
            {
                var sendResult = await client.PutAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/apiscope/edit", data);

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult.Error("Не удалось изменить данные области");

                return result;
            }
            catch
            {
                return AjaxResult.Error($"Произошла внутренняя ошибка");
            }
        }

        public async Task<AjaxResult> EditStatusAsync(EditApiScopeStatusModel data)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(_accessTolen);

            try
            {
                var sendResult = await client.PutAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/apiscope/edit/status", data);

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult.Error("Не удалось изменить статус области");

                return result;
            }
            catch
            {
                return AjaxResult.Error($"Произошла внутренняя ошибка");
            }
        }

        public async Task<AjaxResult<IDApiScope>> FindAsync(int scopeId)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(_accessTolen);

            try
            {
                var sendResult = await client.GetAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/apiscope/{scopeId}");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<IDApiScope>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<IDApiScope>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<IDApiScope>.Error("Не удалось получить данные области");

                return result;
            }
            catch
            {
                return AjaxResult<IDApiScope>.Error($"Произошла внутренняя ошибка");
            }
        }

        public async Task<AjaxResult<IEnumerable<IDApiScope>>> GetAsync(ApiScopeSearchFilter filter)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(_accessTolen);

            try
            {
                var sendResult = await client.GetAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/apiscope?id={filter.Id}&name={filter.Name}");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<IEnumerable<IDApiScope>>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<IEnumerable<IDApiScope>>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<IEnumerable<IDApiScope>>.Error("Не удалось получить данные областей");

                return result;
            }
            catch
            {
                return AjaxResult<IEnumerable<IDApiScope>>.Error($"Произошла внутренняя ошибка");
            }
        }

        public async Task<AjaxResult> RemoveAsync(int scopeId)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(_accessTolen);

            try
            {
                var sendResult = await client.DeleteAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/apiscope/{scopeId}/remove");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult.Error("Не удалось удалить данные области");

                return result;
            }
            catch
            {
                return AjaxResult.Error($"Произошла внутренняя ошибка");
            }
        }
    }
}
