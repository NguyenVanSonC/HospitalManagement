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
            appoinment.ID = appoinmentViewModel.ID;
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

        public static void UpdateComment(this Comment comment, CommentViewModel commentViewModel)
        {
            comment.ID = commentViewModel.ID;
            comment.DoctorID = commentViewModel.DoctorID;
            comment.ParentID = commentViewModel.ParentID;
            comment.Content = commentViewModel.Content;
            comment.Status = commentViewModel.Status;
            comment.CreatedDate = commentViewModel.CreatedDate;
            comment.CreatedBy = commentViewModel.CreatedBy;
            comment.UpdatedBy = commentViewModel.UpdatedBy;
            comment.UpdatedDate = commentViewModel.UpdatedDate;
        }

        public static void UpdateRate(this Rate rate, RateViewModel rateViewModel)
        {
            rate.ID = rateViewModel.ID;
            rate.DoctorID = rateViewModel.DoctorID;
            rate.CountRate = rateViewModel.CountRate;
            rate.Status = rateViewModel.Status;
            rate.CreatedDate = rateViewModel.CreatedDate;
            rate.CreatedBy = rateViewModel.CreatedBy;
            rate.UpdatedBy = rateViewModel.UpdatedBy;
            rate.UpdatedDate = rateViewModel.UpdatedDate;
        }

        public static void UpdateDoctor(this Doctor doctor, DoctorViewModel doctorViewModel)
        {
            doctor.ID = doctorViewModel.ID;
            doctor.Education = doctorViewModel.Education;
            doctor.DepartmentID = doctorViewModel.DepartmentID;
            doctor.Description = doctorViewModel.Description;
            doctor.Alias = doctorViewModel.Alias;
            doctor.Content = doctorViewModel.Content;
            doctor.HomeFlag = doctorViewModel.HomeFlag;
            doctor.HotFlag = doctorViewModel.HotFlag;
            doctor.Status = doctorViewModel.Status;
            doctor.CreatedDate = doctorViewModel.CreatedDate;
            doctor.CreatedBy = doctorViewModel.CreatedBy;
            doctor.UpdatedBy = doctorViewModel.UpdatedBy;
            doctor.UpdatedDate = doctorViewModel.UpdatedDate;
        }
    }
}