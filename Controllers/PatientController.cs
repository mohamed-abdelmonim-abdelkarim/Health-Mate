using clinimanagmanetsystem.DataConection;
using clinimanagmanetsystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinimanagmanetsystem.Controllers
{
    public class PatientController : Controller
    {
        DBCONTEXCT obj=new DBCONTEXCT();
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Index()
        {
            var patients = obj.patients.ToList();
            return View(patients);
        }

        public IActionResult Details(int id)
        {
            var patient = obj.patients.FirstOrDefault(p => p.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        //[HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                obj.patients.Add(patient);
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
            return View(patient);

        }
        public IActionResult Edit(int id)
        {
            var patient = obj.patients.FirstOrDefault(p => p.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                obj.Update(patient);
                obj.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var patient = obj.patients.FirstOrDefault(p => p.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            obj.patients.Remove(patient);
            obj.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
