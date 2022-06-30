using ImtahanLast.DAL;
using ImtahanLast.Models;
using ImtahanLast.Utilize;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanLast.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        public readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;
        public DoctorController(AppDbContext context , IWebHostEnvironment env)
        {
            _context = context;
            _env = env; 
        }
        public ActionResult Index()
        {
            List<Doctors> doctors = _context.Doctors.ToList();
            return View(doctors);
        }

        // GET: DoctorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DoctorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Doctors doctor)
        {
              
            if (!ModelState.IsValid) return View();

            if (doctor.Image !=null)
            {
                if (doctor.Image.CheckSize(500))
                {
                    ModelState.AddModelError("", "Size coxdu");
                    return View();
                }
                if (!doctor.Image.CheckType("image/"))
                {
                    ModelState.AddModelError("", "type sehvdir");
                    return View();

                }
                doctor.ImageUrl = doctor.Image.SaveImg(Path.Combine(_env.WebRootPath, "assets", "DoctorImg"));
            }
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: DoctorController/Edit/5
        public ActionResult Edit(int id)
        {
            Doctors dc = _context.Doctors.Find(id);
            if (dc == null)
            {
                return View();
            }
            return View(dc);
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Doctors doctor)
        {
            if (id != doctor.Id) return View();
            Doctors DbDc = _context.Doctors.Find(id);
            if (DbDc == null)
            {
                return BadRequest();
            }
           
            if (doctor.Image !=null)
            {
                if (doctor.Image.CheckSize(500))
                {
                    ModelState.AddModelError("", "Size coxdu");
                    return View();
                }
                if (!doctor.Image.CheckType("image/"))
                {
                    ModelState.AddModelError("", "type sehvdir");
                    return View();

                }
                FileManager.DeleteFile(Path.Combine(_env.WebRootPath, "assets", "DoctorImg",DbDc.ImageUrl));
                DbDc.ImageUrl = doctor.Image.SaveImg(Path.Combine(_env.WebRootPath ,"assets", "DoctorImg"));

            }
            DbDc.Name = doctor.Name;
            DbDc.Title = doctor.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: DoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            Doctors dc = _context.Doctors.Find(id);
            if (dc.Image !=null)
            {
                 FileManager.DeleteFile(Path.Combine(_env.WebRootPath, "assets", "DoctorImg", dc.ImageUrl));
            }
            _context.Doctors.Remove(dc);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
