using API;
using API.Models;

WHATClient client = new WHATClient();
Lesson lesson = await client.GetLesson(293);
bool lessonIsDone = await client.IsLessonDone(1);
//int addNewLesson = await client.CreateLesson();


Console.WriteLine(lesson.ClassJournal[0].StudentId);
Console.WriteLine(lessonIsDone);
//Console.WriteLine(addNewLesson);