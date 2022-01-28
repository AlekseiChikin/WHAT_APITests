using API;
using API.Models;
using NUnit.Framework;
using System;

namespace APITests
{
    public class Tests
    {
        AddLessonTestSteps systemUnderTest;
        Lesson newLesson;
        [SetUp]
        public void Setup()
        {
            DateTime dateTime = new DateTime(2022, 1, 21, 21, 20, 30, 300);
            systemUnderTest = new AddLessonTestSteps(new User { Email = "james.smith@example.com", Password = "Nj_PJ7K9", Role = "admin" });



            newLesson =
                new LessonBuilder()
                .AddMentorById(systemUnderTest.newMentorInSystem.Id)
                .AddStudentsGroupById(systemUnderTest.newGroupInSystem.Id)
                .AddLessonTheme("FromTets")
                .AddLessonDate(dateTime)
                .AddClassBook(new ClassBook[] {
                    new ClassBook{
                    StudentId = systemUnderTest.newStudent1InSystem.Id,
                    StudentMark = 1,
                    Comment = null!,
                    Presence = true
                    },
                    new ClassBook{
                    StudentId = systemUnderTest.newStudent2InSystem.Id,
                    StudentMark = 3,
                    Comment = null!,
                    Presence = true
                    }
                })
                .Build();
        }

        [Test]
        public void AdminCanAddLesson()
        {
            systemUnderTest
                .VerifyGroupExist()
                .VerifyMentorExist()
                .AddNewLesson(newLesson)
                .VerifyLessonExist();


        }
        [TearDown]
        public void After()
        {
         //delete
        }
    }
}