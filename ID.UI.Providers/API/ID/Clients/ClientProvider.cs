using ID.UI.Core;
using ID.UI.Core.Clients;
using ID.UI.Core.Clients.Abstractions;
using ID.UI.Core.Clients.Models;
using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ID.UI.Providers.API.ID.Clients
{
    public class ClientProvider : IClientProvider
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly ApiOptions _apiOptions;

        protected string _accessToken = string.Empty;

        public ClientProvider
            (IHttpClientFactory httpClientFactory,
             IOptions<ApiOptions> apiDescriptor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiOptions = apiDescriptor?.Value ?? new ApiOptions();
        }

        public void WithAccessToken(string accessToken)
        {
            this._accessToken = accessToken;
        }

        public async Task<AjaxResult<Client>> CreateAsync(CreateClientModel data)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.PostAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/client/create", data);
                
                if(sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<Client>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                var result = JsonConvert.DeserializeObject<AjaxResult<Client>>
                    (await sendResult.Content.ReadAsStringAsync());

                if(result == null)
                {
                    return AjaxResult<Client>.Error($"Не удалось добавить приложение");
                }

                return result;
            }
            catch
            {
                return AjaxResult<Client>.Error($"Произошла внутренняя ошибка");
            }
        }

        public async Task<AjaxResult> EditAsync(EditClientModel data)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.PutAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/client/edit", data);

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult.Error("Доступ запрещён", sendResult.StatusCode);
                }

                var result = JsonConvert.DeserializeObject<AjaxResult>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                {
                    return AjaxResult.Error($"Не удалось изменить данные приложение");
                }

                return result;
            }
            catch
            {
                return AjaxResult.Error($"Произошла внутренняя ошибка");
            }
        }

        public async Task<AjaxResult> EditStatusAsync(EditClientStatusModel data)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.PutAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/client/edit/status", data);

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult.Error("Доступ запрещён", sendResult.StatusCode);
                }

                var result = JsonConvert.DeserializeObject<AjaxResult>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                {
                    return AjaxResult.Error($"Не удалось изменить статус приложения");
                }

                return result;
            }
            catch
            {
                return AjaxResult.Error($"Произошла внутренняя ошибка");
            }
        }

        public async Task<AjaxResult<Client>> FindAsync(string clientId)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.GetAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/client/{clientId}");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<Client>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                var result = JsonConvert.DeserializeObject<AjaxResult<Client>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                {
                    return AjaxResult<Client>.Error($"Не удалось получить данные приложения");
                }

                return result;
            }
            catch
            {
                return AjaxResult<Client>.Error($"Произошла внутренняя ошибка");
            }
        }

        public async Task<AjaxResult<IEnumerable<Client>>> GetAsync(ClientSearchFilter filter)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.GetAsync(_apiOptions.IDUrl.AbsoluteUri +
                    $"api/client?id={filter.Id}&clientId={filter.ClientId}&name={filter.Name}&status={filter.Status}");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<IEnumerable<Client>>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                var result = JsonConvert.DeserializeObject<AjaxResult<IEnumerable<Client>>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                {
                    return AjaxResult<IEnumerable<Client>>.Error($"Не удалось получить данные приложений");
                }

                return result;
            }
            catch
            {
                return AjaxResult<IEnumerable<Client>>.Error($"Произошла внутренняя ошибка");
            }
        }

        public async Task<AjaxResult> RemoveAsync(string clientId)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.DeleteAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/client/{clientId}/remove");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<Client>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                var result = JsonConvert.DeserializeObject<AjaxResult<Client>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                {
                    return AjaxResult<Client>.Error($"Не удалось получить приложение");
                }

                return result;
            }
            catch
            {
                return AjaxResult<Client>.Error($"Произошла внутренняя ошибка");
            }
        }
    }
}
