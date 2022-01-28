using API;
using API.Models;
using System.Net;

HttpStatusCode statusCode;
WHATClient client = new WHATClient(new User { Email = "james.smith@example.com", Password = "Nj_PJ7K9", Role = "admin" });


Uri uri = new Uri($"/api/v2/lessons/{1}", UriKind.Relative);

var response = client.Get<Lesson>(uri, out statusCode);

Console.WriteLine(response.ThemeName);

uri = new Uri($"/api/v1/student_groups/{2}", UriKind.Relative);

var resp = client.Get<GroupDto>(uri, out statusCode);

Console.WriteLine(resp.FinishDate);


//var lessons = await client.POST<Lesson, Lesson>(uri, new Lesson());

//var mentro = await client.POST<Mentor>(uri);



//Lesson lesson = await client.GetLesson(293);
//bool lessonIsDone = await client.IsLessonDone(1);
//int addNewLesson = await client.CreateLesson();



Console.WriteLine(statusCode);
//Console.WriteLine(lessonIsDone);
//Console.WriteLine(addNewLesson);

