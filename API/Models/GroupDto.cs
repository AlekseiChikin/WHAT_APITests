using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    public class GroupDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("courseId")]
        public int CourseId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("startDate")]
        public string StartDate { get; set; }

        [JsonPropertyName("finishDate")]
        public string FinishDate { get; set; }

        [JsonPropertyName("studentIds")]
        public int[] StudentIds { get; set; }

        [JsonPropertyName("mentorIds")]
        public int[] MentorIds { get; set; }
    }
}
