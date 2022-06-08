using System.ComponentModel.DataAnnotations;

namespace Kusys.Models
{
    public class Course
    {
        [Key]
        public string CourseId { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
    }
}
