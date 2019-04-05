using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Data
{
    public class SunShineHospitalDbContext : IdentityDbContext<ApplicationUser>
    {
        public SunShineHospitalDbContext() : base("SunShineHospitalConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static SunShineHospitalDbContext Create()
        {
            return new SunShineHospitalDbContext();
        }

        public DbSet<Doctor> Doctors { set; get; }
        public DbSet<Department> Departments { set; get; }
        public DbSet<Appoinment> Appoinments { set; get; }
        public DbSet<Patient> Patients { set; get; }
        public DbSet<Medicine> Medicines { set; get; }
        public DbSet<Prescription> Prescriptions { set; get; }
        public DbSet<PrescriptionDetail> PrescriptionDetails { set; get; }
        public DbSet<ExaminationDetail> ExaminationDetails { set; get; }
        public DbSet<Examination> Examinations { set; get; }
        public DbSet<Test> Tests { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Rate> Rates { set; get; }
        public DbSet<MedicineCategory> MedicineCategories { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
        }
    }
}
