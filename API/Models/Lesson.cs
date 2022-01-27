using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    public class Lesson
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("themeName")]
        public string ThemeName { get; set; }
        
        [JsonPropertyName("mentorId")]
        public int MentorId { get; set; }
        
        [JsonPropertyName("studentGroupId")]
        public int StudentGroupId { get; set; }
        
        [JsonPropertyName("lessonDate")]
        public DateTime LessonDate { get; set; }
        
        [JsonPropertyName("lessonVisits")]
        public ClassBook[] ClassJournal { get; set; }

    }
    public class ClassBook
    {
        [JsonPropertyName("studentId")]
        public int StudentId { get; set; }

        [JsonPropertyName("studentMark")]
        public int StudentMark { get; set; }

        [JsonPropertyName("presence")]
        public bool Presence { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }


    }

    public class LessonBuilder
    {
        protected Lesson lesson;
        public LessonBuilder() => lesson = new Lesson();

        public LessonBuilder AddLessonTheme(string theme)
        {
            lesson.ThemeName = theme;
            return this;
        }
        public LessonBuilder AddMentorById(int mentorId)
        {
            lesson.MentorId = mentorId;
            return this;
        }
        public LessonBuilder AddStudentsGroupById(int groupId)
        {
            lesson.StudentGroupId = groupId;
            return this;
        }
        public LessonBuilder AddLessonDate(DateTime lessonDate)
        {
            lesson.LessonDate = lessonDate;
            return this;
        }
        public LessonBuilder AddClassBook(ClassBook[] classBook)
        {
            lesson.ClassJournal = classBook;
            return this;
        }
        public Lesson Build()
        {
            //TODO check
            return lesson;
        }

    }
}
