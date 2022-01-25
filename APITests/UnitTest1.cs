using API;
using API.Models;
using NUnit.Framework;

namespace APITests
{
    public class Tests
    {
        Lesson lesson;

        [SetUp]
        public void Setup()
        {

           Lesson newLesson = 
                new LessonBuilder()
                .AddMentorById(1)
                .AddStudentsGroupById(1)
                .AddLessonTheme("asdaaf")
                .AddLessonDate("12.12.2012")
                .Build();
        }

        [Test]
        public void Test1()
        {
            WHATClient client = new WHATClient();
            lesson = client.GetLesson(1).GetAwaiter().GetResult();

            Assert.IsTrue(lesson.Id == 1);
        }
    }
}