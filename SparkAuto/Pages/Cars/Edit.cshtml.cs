using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using Microsoft.AspNetCore.Authorization;

namespace SparkAuto.Pages.Cars
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Car Car { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? Id)
        {

            if (Id == null)
            {
                return NotFound();
            }

            Car = await _db.Car
                .Include(m => m.ApplicationUser)
                .FirstOrDefaultAsync(c => c.Id == Id);

            if(Car == null)
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
            _db.Attach(Car).State = EntityState.Modified;

            await _db.SaveChangesAsync();
            StatusMessage = "Update Car Successfuly";
            return RedirectToPage("./Index" , new { userid = Car.UserId });
        }
    }
}
