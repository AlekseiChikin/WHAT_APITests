using API.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class AddLessonTestSteps
    {
        WHATClient client;
        Uri uri;
        HttpStatusCode statusCode;


        public AccountUser newMentorInSystem;
        public AccountUser newStudent1InSystem;
        public AccountUser newStudent2InSystem;

        public RegisterUserDto newMentor;
        public RegisterUserDto newStudent1;
        public RegisterUserDto newStudent2;

        public GroupDto newGroupInSystem;

        public AddLessonTestSteps(User user)
        {
            client = new WHATClient(user);


            newMentor = new RegisterUserDto
            {
                FirstName = "Firstn",
                LastName = "Lastn",
                Email = "qwerpp8@gmail.com",
                Password = "Qwerty_123",
                ConfirmPassword = "Qwerty_123"
            };
            newStudent1 = new RegisterUserDto
            {
                FirstName = "Firsts",
                LastName = "Lasts",
                Email = "qwerpps8@gmail.com",
                Password = "Qwerty_123",
                ConfirmPassword = "Qwerty_123"
            };
            newStudent2 = new RegisterUserDto
            {
                FirstName = "Firstns",
                LastName = "Lastns",
                Email = "qwerppss8@gmail.com",
                Password = "Qwerty_123",
                ConfirmPassword = "Qwerty_123"
            };
            //newMentorInSystem = CreateMentorInSystem(newMentor);
            //newStudent1InSystem = CreateStudentInSystem(newStudent1);
            //ewStudent2InSystem = CreateStudentInSystem(newStudent2);

            newGroupInSystem = CreateNewGroupInSystem("zaadssdf");

        }
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
        AccountUser CreateStudentInSystem(RegisterUserDto newUser)
        {
            uri = new Uri($"/api/v2/accounts/reg", UriKind.Relative);
            var response = client.POST<RegisterUserDto, AccountUser>(uri, newUser, out statusCode);

            Debug.WriteLine($"On CreateStudentInSystem - Register User code = {statusCode}");

            uri = new Uri($"/api/v2/students/{response.Id}", UriKind.Relative);
            var resp = client.POST<UserInSystem>(uri, out statusCode);

            Debug.WriteLine($"On CreateStudentInSystem - Asign role code = {statusCode}");

            return response;
        }

        GroupDto CreateNewGroupInSystem(string groupName)
        {
            DateTime dateStart = new DateTime(2022, 1 , 1 ,12 , 15 , 30 , 300);
            DateTime dateEnd = new DateTime(2022, 1, 10, 10, 15, 30, 300);

            GroupDto group = new GroupDto
            {
                MentorIds = new int[] { 13 },
                Name = groupName,
                CourseId = 1,
                StudentIds = new int[] { 2 },
                StartDate = "2020-01-25",
                FinishDate = "2022-01-26"
            };

            uri = new Uri($"/api/v2/student_groups", UriKind.Relative);
            var response = client.POST<GroupDto, GroupDto>(uri, group, out statusCode);

            Debug.WriteLine($"On CreateNewGroupInSystem - code = {statusCode}");

            return response;

        }
        public AddLessonTestSteps VerifyGroupExist()
        {
            uri = new Uri($"/api/v2/student_groups/{newGroupInSystem.Id}", UriKind.Relative);
            var response = client.GET<GroupDto>(uri, out statusCode);
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            return this;
        }
        public AddLessonTestSteps VerifyMentorExist()
        {
            uri = new Uri($"/api/v2/mentors/{newMentorInSystem.Id}", UriKind.Relative);
            var response = client.GET<UserInSystem>(uri, out statusCode);
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            return this;
        }

        public AddLessonTestSteps AddNewLesson(Lesson newLesson)
        {
            uri = new Uri($"/api/v2/lessons", UriKind.Relative);
            var response = client.POST<Lesson, Lesson>(uri, newLesson, out statusCode);

            return this;
        }

        public AddLessonTestSteps VerifyLessonExist(int lessonId)
        {
            uri = new Uri($"/api/v2/lessons/{lessonId}", UriKind.Relative);
            var response = client.GET<Lesson>(uri, out statusCode);
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            return this;
        }

        RegisterUserDto GenereteUserForRegistration()
        {
            throw new NotImplementedException();
        }
    }

    
}
