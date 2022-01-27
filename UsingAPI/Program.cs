using API;
using API.Models;
using System.Net;

HttpStatusCode statusCode;
WHATClient client = new WHATClient(new User { Email = "james.smith@example.com", Password = "Nj_PJ7K9", Role = "admin" });
Uri uri = new Uri("/api/v2/accounts/reg", UriKind.Relative);
var response = client.POST<RegisterUserDto, AccountUser>(uri, newUser, out statusCode);




//var lessons = await client.POST<Lesson, Lesson>(uri, new Lesson());

//var mentro = await client.POST<Mentor>(uri);



//Lesson lesson = await client.GetLesson(293);
//bool lessonIsDone = await client.IsLessonDone(1);
//int addNewLesson = await client.CreateLesson();


Console.WriteLine(lessn.ThemeName);
Console.WriteLine(statusCode);
//Console.WriteLine(lessonIsDone);
//Console.WriteLine(addNewLesson);

AccountUser CreateMentorInSystem(RegisterUserDto newUser)
{
    uri = new Uri($"/api/v2/accounts/reg", UriKind.Relative);
    var response = client.POST<RegisterUserDto, AccountUser>(uri, newUser, out statusCode);

    Debug.WriteLine($"On CreateUserInSystem - Register User code = {statusCode}");

    uri = new Uri($"/api/v2/mentors/{response.Id}", UriKind.Relative);
    var resp = client.POST<UserInSystem>(uri, out statusCode);

    Debug.WriteLine($"On CreateUserInSystem - Asign role code = {statusCode}");

    return response;
}