using clinimanagmanetsystem.DataConection;
using clinimanagmanetsystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinimanagmanetsystem.Controllers
{
    public class DoctorController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        DBCONTEXCT obj = new DBCONTEXCT();
        public IActionResult Index()
        {
            var doctors = obj.Doctors
              .Include(d => d.Appointments) 
                    .ToList();
            return View(doctors);
        }
        public IActionResult Details(int id)
        {
            var doctor = obj.Doctors.FirstOrDefault(d => d.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
       // [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                obj.Doctors.Add(doctor);
                obj.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(doctor);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var doctor = obj.Doctors.FirstOrDefault(d => d.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Doctor doctor)
        {
            if (id != doctor.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    obj.Doctors.Update(doctor);
                    obj.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // سجل الأخطاء هنا إن وجدت
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                // سجل الأخطاء في الـ ModelState
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }

            return View(doctor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var doctor = obj.Doctors.FirstOrDefault(d => d.DoctorId == id);
            if (doctor != null)
            {
                obj.Doctors.Remove(doctor);
                obj.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
