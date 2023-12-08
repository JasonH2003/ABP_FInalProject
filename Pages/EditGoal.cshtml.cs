using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProject;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Pages
{
    public class EditGoalModel : PageModel
    {
        private readonly UserDbContext _context;

        public EditGoalModel(UserDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Goals Goal { get; set; }

        public async Task<IActionResult> OnGetAsync(int? goalId)
        {
            if (goalId == null)
            {
                return NotFound();
            }

            Goal = await _context.Goals.Include(g => g.User)
                                       .FirstOrDefaultAsync(m => m.GoalID == goalId);

            if (Goal == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Goal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Details", new { id = Goal.UserID });
        }
    }
}

