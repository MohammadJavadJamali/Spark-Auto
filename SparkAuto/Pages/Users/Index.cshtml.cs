using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Models.ViewModel;
using SparkAuto.Utiltty;

namespace SparkAuto.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public UsersListViewModel usersListVM { get; set; }

        public async Task<IActionResult> OnGet(int productPage = 1, string searchEmail = null, 
            string searchName = null , string searchPhone = null)
        {
            usersListVM = new UsersListViewModel()
            {
                applicationUsersList = await _db.applicationUsers.ToListAsync()
            };

            StringBuilder param = new StringBuilder();
            param.Append("/Users?productPage=:");

            param.Append("&searchName=");
            if (searchName != null)
            {
                param.Append(searchName);
            }
            param.Append("&searchEmail=");
            if (searchEmail != null)
            {
                param.Append(searchEmail);
            }
            param.Append("&searchPhone=");
            if (searchPhone != null)
            {
                param.Append(searchPhone);
            }

            if (searchEmail != null)
            {
                usersListVM.applicationUsersList = await _db.applicationUsers.Where(u => 
                u.Email.ToLower().Contains(searchEmail.ToLower())).ToListAsync();
            }
            else
            {
                if (searchName != null)
                {
                    usersListVM.applicationUsersList = await _db.applicationUsers.Where(u =>
                    u.Name.ToLower().Contains(searchName.ToLower())).ToListAsync();
                }
                else
                {
                    if (searchPhone != null)
                    {
                        usersListVM.applicationUsersList = await _db.applicationUsers.Where(u =>
                        u.PhoneNumber.ToLower().Contains(searchPhone.ToLower())).ToListAsync();
                    }
                }
            }


            var count = usersListVM.applicationUsersList.Count;

            usersListVM.pagingInfo = new PagingInfo()
            {
                CurrentPage = productPage,
                ItemPerPage = SD.PaginationUsersPageSize,
                TotalItems = count,
                UrlParam = param.ToString()
            };
            usersListVM.applicationUsersList = usersListVM.applicationUsersList.OrderBy(p => p.Email)
                .Skip((productPage - 1) * SD.PaginationUsersPageSize)
                .Take(SD.PaginationUsersPageSize).ToList();
            return Page();
        }
    }
}
