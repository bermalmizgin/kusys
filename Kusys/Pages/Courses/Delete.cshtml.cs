using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kusys.Data;
using Kusys.Models;
using Microsoft.AspNetCore.Authorization;

namespace Kusys.Pages.Courses
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Kusys.Data.ApplicationDbContext _context;

        public DeleteModel(Kusys.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FirstOrDefaultAsync(m => m.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }
            else 
            {
                Course = course;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }
            var course = await _context.Course.FindAsync(id);

            if (course != null)
            {
                Course = course;
                _context.Course.Remove(Course);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
