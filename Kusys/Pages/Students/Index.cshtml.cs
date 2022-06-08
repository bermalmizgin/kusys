using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kusys.Data;
using Kusys.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Kusys.Pages.Students
{
    [Authorize(Roles = "Admin,User")]
    public class IndexModel : PageModel
    {
        private readonly Kusys.Data.ApplicationDbContext _context;

        public IndexModel(Kusys.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // User is Admin
            var isAdmin = User.IsInRole("Admin");



            if (_context.Student != null)
            {
                if (isAdmin)
                {
                    Student = await _context.Student
                .Include(s => s.Course)
                .Include(s => s.Users).ToListAsync();
                }
                else
                {
                    // Get UserId by identity
                    Claim? userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier);

                    // Get student by identity
                    var student = _context.Student.FirstOrDefault(a => a.UserId == userId.Value);

                    if (student != null)
                    {
                        // Student list by user student courseId
                        Student = await _context.Student
                            .Include(s => s.Course)
                            .Include(s => s.Users)
                            .Where(a => a.CourseId == student.CourseId)
                            .ToListAsync();
                    }
                }
            }
        }
    }
}
