using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalProject;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Pages
{
    public class CreateModel : PageModel
    {
        private readonly UserDbContext _context;

        public CreateModel(UserDbContext context)
        {
            _context = context;
        }

        

        [BindProperty]
        public User User { get; set; } = new User();

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

        public IActionResult OnGet()
        {
            User = new User();
            User.Goals = new List<Goals> { new Goals(), new Goals() };

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }
            User.Full_Name = Full_Name;
            User.Birth_Date = Birth_Date;
            User.HeightFT = HeightFT;
            User.BodyWeightLbs = BodyWeightLbs;

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
