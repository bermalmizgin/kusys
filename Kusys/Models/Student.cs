using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kusys.Models
{
    public class Student
    {
        public Student()
        {
            
        }

        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        [ForeignKey("Users")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("Course")]
        public string CourseId { get; set; } = string.Empty;

        public virtual Course? Course { get; set; } 
        public virtual IdentityUser? Users { get; set; } 
    }
}
