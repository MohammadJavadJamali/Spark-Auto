using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SparkAuto.Data;
using Microsoft.AspNetCore.Authorization;
using SparkAuto.Models;

namespace SparkAuto.Pages.Cars
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel( ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CarAndCustomerViewModel CarAndCustomerVM { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId = null)
        {

            if(userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }

            CarAndCustomerVM = new CarAndCustomerViewModel()
            {
                Cars = await _db.Car.Where(c => c.UserId == userId).ToListAsync(),
                UserObj = await _db.applicationUsers.FirstOrDefaultAsync( u => u.Id == userId)
            };

            return Page();

        }
    }
}
