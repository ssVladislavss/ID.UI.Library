using ID.UI.Core;
using ID.UI.Core.Options;
using ID.UI.Core.Users;
using ID.UI.Core.Users.Abstractions;
using ID.UI.Core.Users.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ID.UI.Providers.API.ID.Users
{
    public class UserProvider : IUserProvider
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly ApiOptions _apiOptions;

        protected string _accessToken = string.Empty;

        public UserProvider
            (IHttpClientFactory httpClientFactory,
             IOptions<ApiOptions> apiAccessor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiOptions = apiAccessor?.Value ?? new ApiOptions();
        }

        public void WithAccessToken(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<AjaxResult<CreateUserResultModel>> CreateAsync(CreateUserModel data)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.PostAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + "api/user/create", data);

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<CreateUserResultModel>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<CreateUserResultModel>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<CreateUserResultModel>.Error("Не удалось добавить пользователя");

                return result;
            }
            catch
            {
                return AjaxResult<CreateUserResultModel>.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult> DeleteAsync(string userId)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.DeleteAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/user/{userId}/remove");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult.Error("Не удалось удалить данные пользователя");

                return result;
            }
            catch
            {
                return AjaxResult.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult<UserModel>> FindByIdAsync(string userId)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.GetAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/user/{userId}");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<UserModel>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<UserModel>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<UserModel>.Error("Не удалось получить данные пользователя");

                return result;
            }
            catch
            {
                return AjaxResult<UserModel>.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult<IEnumerable<UserModel>>> GetAsync(UserSearchFilter filter)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.GetAsync(_apiOptions.IDUrl.AbsoluteUri + $"api/user?lastName={filter.LastName}&firstName={filter.FirstName}" + 
                    $"&secondName={filter.SecondName}&email={filter.Email}&birthDate={filter.BirthDate}&phone={filter.Phone}" + 
                    $"&role={filter.Role}");

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<IEnumerable<UserModel>>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<IEnumerable<UserModel>>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<IEnumerable<UserModel>>.Error("Не удалось получить данные пользователей");

                return result;
            }
            catch
            {
                return AjaxResult<IEnumerable<UserModel>>.Error("Произошла неизвестная ошибка");
            }
        }

        public async Task<AjaxResult> UpdateAsync(EditUserModel data)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.PutAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + "api/user/edit", data);

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

        public async Task<AjaxResult<DateTimeOffset?>> SetLockoutEnabledAsync(SetLockoutEnabledModel data)
        {
            using var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(this._accessToken);

            try
            {
                var sendResult = await client.PutAsJsonAsync(_apiOptions.IDUrl.AbsoluteUri + "api/user/set/lockout", data);

                if (sendResult.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                   sendResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return AjaxResult<DateTimeOffset?>.Error("Доступ запрещён", sendResult.StatusCode);
                }

                sendResult.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<AjaxResult<DateTimeOffset?>>
                    (await sendResult.Content.ReadAsStringAsync());

                if (result == null)
                    return AjaxResult<DateTimeOffset?>.Error("Не удалось установить статус аккаунта");

                return result;
            }
            catch
            {
                return AjaxResult<DateTimeOffset?>.Error("Произошла неизвестная ошибка");
            }
        }
    }
}
