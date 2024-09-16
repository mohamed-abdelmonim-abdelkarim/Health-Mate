using clinimanagmanetsystem.DataConection;
using clinimanagmanetsystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinimanagmanetsystem.Controllers
{
    public class MedicalRecordController : Controller
    {
        
        DBCONTEXCT obj = new DBCONTEXCT();
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Index()
        {
            var medicalRecords = obj.MedicalRecords
           .Include(m => m.Patient) // Include related patient data
           .ToList();
            return View(medicalRecords);
        }


        public IActionResult Create()
        {
            var patients = obj.patients.ToList();
            if (patients == null || !patients.Any())
            {
                // يمكنك تسجيل رسالة في السجل أو معالجة الحالة حسب الحاجة
                ViewBag.Patients = new List<Patient>(); // تعيين قائمة فارغة إذا لم يكن هناك مرضى
            }
            else
            {
                ViewBag.Patients = patients;
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                obj.MedicalRecords.Add(medicalRecord);
                obj.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(medicalRecord);
        }





        public IActionResult Edit(int id)
        {
            var medicalRecord = obj.MedicalRecords
                .FirstOrDefault(mr => mr.MedicalRecordId == id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return View(medicalRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.MedicalRecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                obj.MedicalRecords.Update(medicalRecord);
                obj.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(medicalRecord);
        }

        public IActionResult Delete(int id)
        {
            var record = obj.MedicalRecords
                .Include(m => m.Patient)
                .FirstOrDefault(m => m.MedicalRecordId == id);
            if (record == null)
            {
                return NotFound();
            }
            obj.MedicalRecords.Remove(record);
            obj.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
