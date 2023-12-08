using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Pages;

public class UserModel : PageModel
{
    private readonly UserDbContext _context;
    private readonly ILogger<UserModel> _logger;
    public List<User> Users {get;set;} = default!;
    public List<ScheduledLift> DailyLifts {get;set;} = default!;

    public UserModel(UserDbContext context, ILogger<UserModel> logger)
    {
       _context = context;
        _logger = logger;
    }

    public void OnGet()
    {
        Users = _context.Users.ToList();
       
       // Get today's date
            DateTime today = DateTime.UtcNow.Date;

            // Filter ScheduledLift records for today's date
            DailyLifts = _context.ScheduledLifts
                .Where(e => e.LiftDate.Year == today.Year &&
                            e.LiftDate.Month == today.Month &&
                            e.LiftDate.Day == today.Day)
                .ToList();
    }
}
