using clinimanagmanetsystem.Models;
using Microsoft.EntityFrameworkCore;

namespace clinimanagmanetsystem.DataConection
{
    public class DBCONTEXCT:DbContext
    {
        public DbSet<Patient> patients { set; get; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        //public DbSet<Bill> Bills { get; set; }
        //public DBCONTEXCT(DbContextOptions<DBCONTEXCT> options) : base(options)
        //{
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server =.;Database=mvc_clici_manage;Integrated Security=True;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
