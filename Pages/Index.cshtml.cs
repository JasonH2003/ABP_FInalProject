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
    public class IndexModel : PageModel
    {
        private readonly UserDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        public List<User> Users { get;set; } = default!;
        public IndexModel(UserDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        

        public void OnGet()
        {
            Users =  _context.Users.ToList();
        }
    }
}
