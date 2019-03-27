using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SunShineHospital.Model.Models;
using  SunShineHospital.Model;
using SunShineHospital.Models;

namespace SunShineHospital.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateAppoinment(this Appoinment appoinment, AppoinmentViewModel appoinmentViewModel)
        {
            appoinment.DoctorID = appoinmentViewModel.DoctorID;
            appoinment.PatientID = appoinmentViewModel.PatientID;
            appoinment.FullName = appoinmentViewModel.FullName;
            appoinment.Address = appoinmentViewModel.Address;
            appoinment.BirthDay = appoinmentViewModel.BirthDay;
            appoinment.Gender = appoinmentViewModel.Gender;
            appoinment.Healthinsurance = appoinmentViewModel.Healthinsurance;
            appoinment.PhoneNumber = appoinmentViewModel.PhoneNumber;
            appoinment.Email = appoinmentViewModel.Email;
            appoinment.Status = appoinmentViewModel.Status;
            appoinment.CreatedDate = appoinmentViewModel.CreatedDate;
            appoinment.CreatedBy = appoinmentViewModel.CreatedBy;
            appoinment.UpdatedBy = appoinmentViewModel.UpdatedBy;
            appoinment.UpdatedDate = appoinmentViewModel.UpdatedDate;
        }
    }
}