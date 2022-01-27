using API.Models;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace API
{
    public class WHATAuth : AuthenticatorBase
    {
        readonly string BaseUrl = "https://charliebackendapihosting.azurewebsites.net";
        readonly User User;



        public WHATAuth(User user) : base("")
        {
            User = user;
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);
        }

        async Task<string> GetToken()
        {
            var options = new RestClientOptions(BaseUrl);
            using var client = new RestClient(options);


            var request = new RestRequest("api/v2/accounts/auth", Method.Post) { RequestFormat = DataFormat.Json };
            request.AddJsonBody<Authentication>(new Authentication { UserEmail = User.Email, UserPassword = User.Password });

            
            var response = await client.PostAsync<TokenResponse>(request);

            return response!.RoleAndToken.ContainsKey(User.Role) ? response!.RoleAndToken[User.Role] : "";
        }
    }

    public class WHATClient : IDisposable 
    { 
        readonly RestClient _client;

        public WHATClient(User user)
        {
            var options = new RestClientOptions("https://charliebackendapihosting.azurewebsites.net");

            _client = new RestClient(options)
            {
                Authenticator = new WHATAuth(user)
            };
        }

        public T GET<T>(Uri uri, out HttpStatusCode statusCode) 
            where T : class
        {
            var req = new RestRequest(uri, Method.Get);
            
            var response =  _client.ExecuteAsync<T>(req).GetAwaiter().GetResult();
            
            statusCode = response.StatusCode;

            return response.IsSuccessful ? response.Data! : default!;
        }


        public U POST<T,U>(Uri uri, T body, out HttpStatusCode statusCode) 
            where T : class 
            where U : class
        {
            var req = new RestRequest(uri, Method.Post);
            
            req.AddJsonBody<T>(body);

            var response =  _client.ExecutePostAsync<U>(req).GetAwaiter().GetResult();
            
            statusCode = response.StatusCode;
            
            return response.IsSuccessful ? response.Data! : default!;
        }


        public T POST<T>(Uri uri, out HttpStatusCode statusCode)
            where T : class
        {
            var req = new RestRequest(uri, Method.Post);

            var response =  _client.ExecutePostAsync<T>(req).GetAwaiter().GetResult();
            
            statusCode = response.StatusCode;
            
            return response.IsSuccessful ? response.Data! : default!;
        }


        public U PUT<T, U>(Uri uri, T body, out HttpStatusCode statusCode) 
            where T : class
            where U : class
        {
            var req = new RestRequest(uri, Method.Put);

            req.AddJsonBody<T>(body);

            var response = _client.ExecutePutAsync<U>(req).GetAwaiter().GetResult();
            
            statusCode = response.StatusCode;
            
            return response.IsSuccessful ? response.Data! : default!;
        }

        public T DELETE<T>(Uri uri, out HttpStatusCode statusCode) 
            where T : class
        {
            var req = new RestRequest(uri, Method.Delete);

            var response =  _client.ExecuteAsync<T>(req).GetAwaiter().GetResult();

            statusCode = response.StatusCode;

            return response.IsSuccessful ? response.Data! : default!;
        }


        public U PATCH<T, U>(Uri uri, T body, out HttpStatusCode statusCode) 
            where T : class
            where U : class
        {
            var req = new RestRequest(uri, Method.Patch);

            req.AddJsonBody<T>(body);

            var response = _client.ExecutePutAsync<U>(req).GetAwaiter().GetResult(); ;

            statusCode = response.StatusCode;

            return response.IsSuccessful ? response.Data! : default!;
        }






        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}