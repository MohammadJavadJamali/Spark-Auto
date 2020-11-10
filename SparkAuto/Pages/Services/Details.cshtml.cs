using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using Microsoft.AspNetCore.Authorization;
using SparkAuto.Models;

namespace SparkAuto.Pages.Services
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public ServiceHeader ServiceHeader { get; set; }
        public List<ServiceDetails> ServiceDetails { get; set; }

        public void OnGet(int serviceId)
        {
            ServiceHeader = _db
                .serviceHeaders
                .Include(c => c.Car)
                .Include(c => c.Car.ApplicationUser)
                .FirstOrDefault(c => c.Id == serviceId);


            ServiceDetails = _db
                .serviceDetails
                .Where(c => c.ServiceHeaderId == serviceId)
                .ToList();


        }
    }
}
