using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Pages
{
    public class WorkoutModel : PageModel
    {
        private readonly UserDbContext _context;
        public List<Workouts> Workouts { get; set; } = default!;
        public string CurrentFilter { get; set; } = string.Empty;
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public WorkoutModel(UserDbContext context)
        {
            _context = context;
        }

        

        public void OnGet(string searchString, int pageIndex = 1)
    {
        const int pageSize = 10;

        IQueryable<Workouts> workoutsQuery = _context.Workouts
                                            .Include(w => w.Difficulty)
                                            .Include(w => w.MuscleGroups);

        // Filtering by search string
        if (!string.IsNullOrEmpty(searchString))
        {
            workoutsQuery = workoutsQuery.Where(w =>
                w.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        int totalWorkouts = workoutsQuery.Count();

        // Pagination
        Workouts = workoutsQuery.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .OrderBy(e => e.WorkoutID)
                                .ToList();

            // Disable next/prev buttons as appropriate
            bool hasPrevious = pageIndex > 1;
            bool hasNext = (pageIndex * pageSize) < totalWorkouts;

            // You can pass these to the view using ViewData or create properties in the model
            ViewData["HasPrevious"] = hasPrevious;
            ViewData["HasNext"] = hasNext;
            ViewData["PageIndex"] = pageIndex;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)totalWorkouts / pageSize);
        }
    }
}
