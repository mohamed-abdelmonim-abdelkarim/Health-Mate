using clinimanagmanetsystem.DataConection;
using clinimanagmanetsystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinimanagmanetsystem.Controllers
{
    public class ApointmentController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        DBCONTEXCT obj = new DBCONTEXCT();
        public IActionResult Index()
        {
            var appointments = obj.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToList();
            return View(appointments);
        }
        public IActionResult Create()
        {
            ViewBag.Patients = obj.patients.ToList();
            ViewBag.Doctors = obj.Doctors.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                obj.Appointments.Add(appointment);
                obj.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Patients = obj.patients.ToList();
            ViewBag.Doctors = obj.Doctors.ToList();
            return View(appointment);
        }
        public IActionResult Edit(int id)
        {
            var appointment = obj.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            ViewBag.Patients = obj.patients.ToList();
            ViewBag.Doctors = obj.Doctors.ToList();
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                obj.Update(appointment);
                obj.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = obj.patients.ToList();
            ViewBag.Doctors = obj.Doctors.ToList();
            return View(appointment);
        }
        public IActionResult Delete(int id)
        {
            var appointment = obj.Appointments.FirstOrDefault(a => a.AppointmentId == id);

            if (appointment != null)
            {
                obj.Appointments.Remove(appointment);
                obj.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
