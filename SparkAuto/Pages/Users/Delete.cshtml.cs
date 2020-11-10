using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Utiltty;

namespace SparkAuto.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class DeleteModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string Id)
        {

            ApplicationUser = await _db.applicationUsers.FirstOrDefaultAsync( u => u.Id == Id);

            if(ApplicationUser == null)
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
            else
            {
                var userInDb = await _db.applicationUsers.FirstOrDefaultAsync(u => 
                u.Id == ApplicationUser.Id);

                if(userInDb == null)
                {
                    return NotFound();
                }
                else
                {
                    _db.Entry(userInDb).State = EntityState.Deleted;

                    await _db.SaveChangesAsync(); 

                    return RedirectToPage("Index");
                }
            }
        }
    }
}
