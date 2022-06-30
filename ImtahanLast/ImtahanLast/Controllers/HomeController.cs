using ImtahanLast.DAL;
using ImtahanLast.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanLast.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Doctors> doctors = _context.Doctors.ToList();
            return View(doctors);
        }
    }
}
