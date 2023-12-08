using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProject;

namespace FinalProject.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly FinalProject.UserDbContext _context;

        public DetailsModel(FinalProject.UserDbContext context)
        {
            _context = context;
        }

        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(m => m.Maxes)
                                            .Include(m => m.Goals)
                                            .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }
    }
}
