using API.Models;
using NUnit.Framework;
using System.Diagnostics;
using System.Net;

namespace API
{
    public class AddLessonTestSteps
    {
        WHATClient client;
        Uri uri;
        HttpStatusCode statusCode;


        public UserInSystem newMentorInSystem;
        public UserInSystem newStudent1InSystem;
        public UserInSystem newStudent2InSystem;

        public RegisterUserDto newMentor;
        public RegisterUserDto newStudent1;
        public RegisterUserDto newStudent2;

        public GroupDto newGroupInSystem;

        public Lesson newLessonInSystem;

        public AddLessonTestSteps(User user)
        {
            client = new WHATClient(user);


            newMentor = new RegisterUserDto
            {
                FirstName = "Firstn",
                LastName = "Lastn",
                Email = "qwerpp15@gmail.com",
                Password = "Qwerty_123",
                ConfirmPassword = "Qwerty_123"
            };
            newStudent1 = new RegisterUserDto
            {
                FirstName = "Firsts",
                LastName = "Lasts",
                Email = "qwerpps15@gmail.com",
                Password = "Qwerty_123",
                ConfirmPassword = "Qwerty_123"
            };
            newStudent2 = new RegisterUserDto
            {
                FirstName = "Firstns",
                LastName = "Lastns",
                Email = "qwerppss15@gmail.com",
                Password = "Qwerty_123",
                ConfirmPassword = "Qwerty_123"
            };
            newMentorInSystem = CreateMentorInSystem(newMentor);
            newStudent1InSystem = CreateStudentInSystem(newStudent1);
            newStudent2InSystem = CreateStudentInSystem(newStudent2);

            newGroupInSystem = CreateNewGroupInSystem("GroupTests2");

        }
        UserInSystem CreateMentorInSystem(RegisterUserDto newUser)
        {
            uri = new Uri($"/api/v2/accounts/reg", UriKind.Relative);
            var response = client.Post<RegisterUserDto, AccountUser>(uri, newUser, out statusCode);

            Debug.WriteLine($"On CreateUserInSystem - Register User code = {statusCode}");

            uri = new Uri($"/api/v2/mentors/{response.Id}", UriKind.Relative);
            var resp = client.Post<UserInSystem>(uri, out statusCode);

            Debug.WriteLine($"On CreateUserInSystem - Asign role code = {statusCode}");

            return resp;
        }
        UserInSystem CreateStudentInSystem(RegisterUserDto newUser)
        {
            uri = new Uri($"/api/v2/accounts/reg", UriKind.Relative);
            var response = client.Post<RegisterUserDto, AccountUser>(uri, newUser, out statusCode);

            Debug.WriteLine($"On CreateStudentInSystem - Register User code = {statusCode}");

            uri = new Uri($"/api/v2/students/{response.Id}", UriKind.Relative);
            var resp = client.Post<UserInSystem>(uri, out statusCode);

            Debug.WriteLine($"On CreateStudentInSystem - Asign role code = {statusCode}");

            return resp;
        }

        GroupDto CreateNewGroupInSystem(string groupName)
        {
            DateTime dateStart = new DateTime(2022, 1 , 1 ,12 , 15 , 30 , 300);
            DateTime dateEnd = new DateTime(2022, 1, 10, 10, 15, 30, 300);

            GroupPostDto group = new GroupPostDto
            {
                MentorIds = new int[] { newMentorInSystem.Id },
                Name = groupName,
                CourseId = 1,
                StudentIds = new int[] { newStudent1InSystem.Id, newStudent2InSystem.Id },
                StartDate = "2020-01-25",
                FinishDate = "2022-01-26"
            };

            uri = new Uri($"/api/v2/student_groups", UriKind.Relative);
            var response = client.Post<GroupPostDto, GroupDto>(uri, group, out statusCode);

            Debug.WriteLine($"On CreateNewGroupInSystem - code = {statusCode}");

            return response;

        }
       
        public AddLessonTestSteps VerifyGroupExist()
        {
            uri = new Uri($"/api/v2/student_groups/{newGroupInSystem.Id}", UriKind.Relative);
            var response = client.Get<GroupDto>(uri, out statusCode);
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            return this;
        }
        public AddLessonTestSteps VerifyMentorExist()
        {
            uri = new Uri($"/api/v2/mentors/{newMentorInSystem.Id}", UriKind.Relative);
            var response = client.Get<UserInSystem>(uri, out statusCode);
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            return this;
        }

        public AddLessonTestSteps AddNewLesson(Lesson newLesson)
        {
            uri = new Uri($"/api/v2/lessons", UriKind.Relative);
            var response = client.Post<Lesson, Lesson>(uri, newLesson, out statusCode);

            newLessonInSystem = response;

            return this;
        }

        public AddLessonTestSteps VerifyLessonExist()
        {
            uri = new Uri($"/api/v2/lessons/{newLessonInSystem.Id}", UriKind.Relative);
            var response = client.Get<Lesson>(uri, out statusCode);
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            return this;
        }

        RegisterUserDto GenereteUserForRegistration()
        {
            throw new NotImplementedException();
        }
    }

    
}
