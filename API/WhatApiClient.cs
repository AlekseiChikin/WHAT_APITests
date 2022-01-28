using API.Models;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace API
{
    

    public class WHATClient : IDisposable 
    { 
        readonly RestClient client;
        string token;

        public WHATClient(User user)
        {
            var options = new RestClientOptions("https://charliebackendapihosting.azurewebsites.net");

            client = new RestClient(options);

            SetToken(user);
        }
        public void SetToken(User user)
        {
            var request = new RestRequest("api/v2/accounts/auth", Method.Post) { RequestFormat = DataFormat.Json };
            request.AddJsonBody<Authentication>(new Authentication { UserEmail = user.Email, UserPassword = user.Password });
            var response = client.PostAsync<TokenResponse>(request).GetAwaiter().GetResult();
            token = response!.RoleAndToken.ContainsKey(user.Role) ? response!.RoleAndToken[user.Role] : "";
        }

        public TResponse Get<TResponse>(Uri uri, out HttpStatusCode statusCode) 
            where TResponse : class
        {
            var req = new RestRequest(uri, Method.Get);
            req.AddOrUpdateHeader("authorization", token);
            var response =  client.ExecuteAsync<TResponse>(req).GetAwaiter().GetResult();
            
            statusCode = response.StatusCode;

            return response.IsSuccessful ? response.Data! : default!;
        }


        public TResponse Post<TParametr,TResponse>(Uri uri, TParametr body, out HttpStatusCode statusCode) 
            where TParametr : class 
            where TResponse : class
        {
            var req = new RestRequest(uri, Method.Post);
            req.AddOrUpdateHeader("authorization", token);
            req.AddJsonBody<TParametr>(body);

            var response =  client.ExecutePostAsync<TResponse>(req).GetAwaiter().GetResult();
            
            statusCode = response.StatusCode;
            
            return response.IsSuccessful ? response.Data! : default!;
        }


        public TResponse Post<TResponse>(Uri uri, out HttpStatusCode statusCode)
            where TResponse : class
        {
            var req = new RestRequest(uri, Method.Post);
            req.AddOrUpdateHeader("authorization", token);
            var response =  client.ExecutePostAsync<TResponse>(req).GetAwaiter().GetResult();
            
            statusCode = response.StatusCode;
            
            return response.IsSuccessful ? response.Data! : default!;
        }


        public TResponse Put<TParametr, TResponse>(Uri uri, TParametr body, out HttpStatusCode statusCode) 
            where TParametr : class
            where TResponse : class
        {
            var req = new RestRequest(uri, Method.Put);
            req.AddOrUpdateHeader("authorization", token);
            req.AddJsonBody<TParametr>(body);

            var response = client.ExecutePutAsync<TResponse>(req).GetAwaiter().GetResult();
            
            statusCode = response.StatusCode;
            
            return response.IsSuccessful ? response.Data! : default!;
        }

        public TParametr Delete<TParametr>(Uri uri, out HttpStatusCode statusCode) 
            where TParametr : class
        {
            var req = new RestRequest(uri, Method.Delete);
            req.AddOrUpdateHeader("authorization", token);
            var response =  client.ExecuteAsync<TParametr>(req).GetAwaiter().GetResult();

            statusCode = response.StatusCode;

            return response.IsSuccessful ? response.Data! : default!;
        }


        public TResponse Patch<TParametr, TResponse>(Uri uri, TParametr body, out HttpStatusCode statusCode) 
            where TParametr : class
            where TResponse : class
        {
            var req = new RestRequest(uri, Method.Patch);
            req.AddOrUpdateHeader("authorization", token);
            req.AddJsonBody<TParametr>(body);

            var response = client.ExecutePutAsync<TResponse>(req).GetAwaiter().GetResult(); ;

            statusCode = response.StatusCode;

            return response.IsSuccessful ? response.Data! : default!;
        }


        public void Dispose()
        {
            client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}