using API;
using API.Models;
using NUnit.Framework;
using System;

namespace APITests
{
    public class Tests
    {
        AddLessonTestSteps preReq;
        Lesson newLesson;
       [SetUp]
        public void Setup()
        {
            DateTime dateTime = new DateTime(2022, 1, 20, 20, 20, 30, 300);
            preReq = new AddLessonTestSteps(new User { Email = "james.smith@example.com", Password = "Nj_PJ7K9", Role = "admin" });



            //newLesson = 
            //    new LessonBuilder()
            //    .AddMentorById(preReq.newMentorInSystem.Id)
            //    .AddStudentsGroupById(preReq.newGroupInSystem.Id)
            //    .AddLessonTheme("llll")
            //    .AddLessonDate(dateTime)
            //    .Build();
        }

        [Test]
        public void AdminCanAddLesson()
        {
            preReq
                .VerifyGroupExist();
                //.VerifyMentorExist()
                //.AddNewLesson()
                //.VerifyLessonExist(newLesson.Id);


        }
        [TearDown]
        public void After()
        {
         //delete
        }
    }
}