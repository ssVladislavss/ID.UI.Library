using ID.UI.Core;
using ID.UI.Core.ApiResources;
using ID.UI.Core.ApiResources.Abstractions;
using ID.UI.Core.ApiResources.Models;
using ID.UI.Core.Options;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ID.UI.Providers.API.ID.ApiResources
{
    public class ApiResourceProvider : IApiResorceProvider
    {
        protected readonly IHttpClientFactory _clientFactory;
        protected readonly ApiOptions _apiOptions;

        protected string _accessToken = string.Empty;

        public ApiResourceProvider
            (IHttpClientFactory clientFactory,
             IOptions<ApiOptions> apiDescriptors)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _apiOptions = apiDescriptors?.Value ?? new ApiOptions();
        }

        public void WithAccessToken(string accessToken)
            => this._accessToken = accessToken;

        public async Task<AjaxResult<IDApiResource>> CreateAsync(CreateApiResourceModel data)
        {
            using var client = _clientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.PostAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + "api/apiresource/create", data);

                if(sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized || 
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<IDApiResource>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<IDApiResource>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<IDApiResource>.Error("Не удалось добавить приложение");

                return result;
            }
            catch
            {
                return AjaxResult<IDApiResource>.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult> EditAsync(EditApiResourceModel data)
        {
            using var client = _clientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.PutAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + "api/apiresource/edit", data);

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult.Error("Не удалось обновить данные приложения");

                return result;
            }
            catch
            {
                return AjaxResult.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult> EditStatusAsync(EditApiResourceStatusModel data)
        {
            using var client = _clientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.PutAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + "api/apiresource/edit/status", data);

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult.Error("Не удалось изменить статус приложения");

                return result;
            }
            catch
            {
                return AjaxResult.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult<IDApiResource>> FindAsync(int resourceId)
        {
            using var client = _clientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.GetAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/apiresource/{resourceId}");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<IDApiResource>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<IDApiResource>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<IDApiResource>.Error("Не удалось получить данные ресурсов");

                return result;
            }
            catch
            {
                return AjaxResult<IDApiResource>.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult<IEnumerable<IDApiResource>>> GetAsync(ApiResourceSearchFilter filter)
        {
            using var client = _clientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.GetAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/apiresource?id={filter.Id}&name={filter.Name}");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<IEnumerable<IDApiResource>>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<IEnumerable<IDApiResource>>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<IEnumerable<IDApiResource>>.Error("Не удалось получить данные ресурсов");

                return result;
            }
            catch
            {
                return AjaxResult<IEnumerable<IDApiResource>>.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult> RemoveAsync(int resourceId)
        {
            using var client = _clientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.DeleteAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/apiresource/{resourceId}/remove");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult.Error("Не удалось удалить данные ресурсов");

                return result;
            }
            catch
            {
                return AjaxResult.Error("Произошла неизвестная ошибка");
            }
        }
    }
}
