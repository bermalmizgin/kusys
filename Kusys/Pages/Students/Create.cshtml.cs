using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kusys.Data;
using Kusys.Models;
using Microsoft.AspNetCore.Authorization;

namespace Kusys.Pages.Students
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly Kusys.Data.ApplicationDbContext _context;

        public CreateModel(Kusys.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseName");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Student == null || Student == null)
            {
                return Page();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            _context.Student.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
