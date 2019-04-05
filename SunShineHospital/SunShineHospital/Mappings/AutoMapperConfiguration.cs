using AutoMapper;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;

namespace TeduShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>();
                cfg.CreateMap<Department, DepartmentViewModel>();
                cfg.CreateMap<Doctor, DoctorViewModel>();
                cfg.CreateMap<Examination, ExaminationViewModel>();
                cfg.CreateMap<ExaminationDetail, ExaminationDetailViewModel>();
                cfg.CreateMap<Appoinment, AppoinmentViewModel>();
                cfg.CreateMap<Medicine, MedicineViewModel>();
                cfg.CreateMap<Patient, PatientViewModel>();
                cfg.CreateMap<Prescription, PrescriptionViewModel>();
                cfg.CreateMap<PrescriptionDetail, PrescriptionDetailsViewModel>();
                cfg.CreateMap<Test, TestViewModel>();
                cfg.CreateMap<Comment, CommentViewModel>();
                cfg.CreateMap<Rate, RateViewModel>();
            });
        }
    }
}