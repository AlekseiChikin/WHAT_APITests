using API.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace API
{
    public class WHATAuth : AuthenticatorBase
    {
        string _baseUrl = "https://charliebackendapihosting.azurewebsites.net";

        public WHATAuth() : base("")
        {
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);
        }

        async Task<string> GetToken()
        {
            var options = new RestClientOptions(_baseUrl);
            using var client = new RestClient(options);


            var request = new RestRequest("api/v2/accounts/auth", Method.Post) { RequestFormat = DataFormat.Json };
            request.AddJsonBody<Authentication>(new Authentication { UserEmail = "james.smith@example.com", UserPassword = "Nj_PJ7K9" });

            
            var response = await client.PostAsync<TokenResponse>(request);
            //TODO проверека 
            return $"{response!.RoleAndToken["admin"]}";
        }
    }

    public class WHATClient :  IDisposable
    {
        readonly RestClient _client;

        public WHATClient()
        {
            var options = new RestClientOptions("https://charliebackendapihosting.azurewebsites.net");

            _client = new RestClient(options)
            {
                Authenticator = new WHATAuth()
            };
        }

        public async Task<Lesson> GetLesson(int id)
        {
            
            var response = await _client.GetJsonAsync<Lesson>(
                "api/v2/lessons/{id}",
                new { id }
            );
            return response!;
        }
        public async Task<int> CreateLesson()
        {
            
            var response = await _client.PostJsonAsync<Lesson, Lesson>(
                "api/v2/lessons",
                new Lesson
                {
                    ThemeName = "FromVSWithResponse",
                    MentorId = 1,
                    StudentGroupId = 1,
                    LessonDate = "2022-01-25T10:00:34.167Z",
                    ClassJournal = new ClassBook[] {
                        new ClassBook {
                            StudentId = 2,
                            StudentMark = 0,
                            Presence = true,
                            Comment = "asd"
                        },
                        new ClassBook {
                            StudentId = 3,
                            StudentMark = 0,
                            Presence = true,
                            Comment = "asd"
                        },
                        new ClassBook {
                            StudentId = 17,
                            StudentMark = 0,
                            Presence = true,
                            Comment = "asd"
                        },
                        new ClassBook {
                            StudentId = 18,
                            StudentMark = 0,
                            Presence = true,
                            Comment = "asd"
                        },
                        new ClassBook {
                            StudentId = 16,
                            StudentMark = 0,
                            Presence = true,
                            Comment = "asd"
                        },
                        new ClassBook {
                            StudentId = 4,
                            StudentMark = 0,
                            Presence = true,
                            Comment = "asd"
                        }

                    }
                }
            );
            return response!.Id;
        }

        public async Task<bool> IsLessonDone(int id)
        {
            var response = await _client.GetJsonAsync<LessonIsDone>(
                "api/v2/lessons/{id}/isdone",
                new { id }
            );
            return response!.isDone;
        }


        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}