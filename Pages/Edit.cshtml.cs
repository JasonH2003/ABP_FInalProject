using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FinalProject.Pages;
public class EditModel : PageModel
{
    private readonly FinalProject.UserDbContext _context;

    public EditModel(FinalProject.UserDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public User User { get; set; } = default!;

    [BindProperty]
    [StringLength(60, MinimumLength = 5)]
    [Display(Name = "Full Name")]
    [Required]
    public string Full_Name { get; set; } = string.Empty;

    [BindProperty]
    [Display(Name = "Birth Date (mm/dd/yyyy)")]
    [Required]
    public DateOnly Birth_Date { get; set; } = new DateOnly();

    [BindProperty]
    [Display(Name = "Height in Ft (ft.in)")]
    [Required]
    public double HeightFT { get; set; }

    [BindProperty]
    [Display(Name = "Body Weight (Lbs)")]
    [Required]
    public double BodyWeightLbs { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        User = await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);
        
        if (User == null)
        {
            return NotFound();
        }

        // Assign values to individual properties
        Full_Name = User.Full_Name;
        Birth_Date = User.Birth_Date;
        HeightFT = User.HeightFT;
        BodyWeightLbs = User.BodyWeightLbs;
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // if (!ModelState.IsValid)
        // {
        //     return Page();`
        // }

        // Update the User object properties
        User.Full_Name = Full_Name;
        User.Birth_Date = Birth_Date;
        User.HeightFT = HeightFT;
        User.BodyWeightLbs = BodyWeightLbs;

        _context.Attach(User).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(User.UserID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.UserID == id);
    }
}
