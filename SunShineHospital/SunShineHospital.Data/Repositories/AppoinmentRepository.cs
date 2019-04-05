using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Data.Repositories
{
    public interface IAppoinmentRepository : IRepository<Appoinment>
    {
       IEnumerable<Appoinment> GetByDoctorId(int doctorId);

       Appoinment GetAppoinmentByPatientId(int? patientId);

        IEnumerable<Appoinment> GetAppoinmentByDoctorId(int doctorId);
    }
    public class AppoinmentRepository : RepositoryBase<Appoinment>, IAppoinmentRepository
    {
        public AppoinmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Appoinment> GetAppoinmentByDoctorId(int doctorId)
        {
            List<Appoinment> listAppoinments = new List<Appoinment>();
            var query = from app in DbContext.Appoinments.Include("Patient")
                where app.DoctorID == doctorId && app.Status == true
                select app;
            foreach (var app in query)
            {
                if (GetDateFromAppoinment(app) > DateTime.Now)
                {
                    listAppoinments.Add(app);
                }
            }

            return listAppoinments.Where(app => app.Status).OrderBy(m => GetDateFromAppoinment(m));
        }


        public Appoinment GetAppoinmentByPatientId(int? patientId)
        {
            var query = from app in DbContext.Appoinments
                where app.PatientID == patientId
                select app;
            foreach (var app in query)
            {
                string stringDate = app.Day + " " + app.Time;
                DateTime dateTimeOfPatient = DateTime.ParseExact(stringDate, "yyyy-MM-dd HH:mm", null);
                if (dateTimeOfPatient > DateTime.Now)
                {
                    return app;
                }
            }

            return null;
        }

        public IEnumerable<Appoinment> GetByDoctorId(int doctorId)
        {
            var query = from app in DbContext.Appoinments
                where app.DoctorID == doctorId
                select app;
            return query.ToList();
        }

        private DateTime GetDateFromAppoinment(Appoinment appoinment)
        {
            string stringDate = appoinment.Day + " " + appoinment.Time;
            DateTime dateTimeOfAppoinment = DateTime.ParseExact(stringDate, "yyyy-MM-dd HH:mm", null);
            return dateTimeOfAppoinment;
        }
    }
}
