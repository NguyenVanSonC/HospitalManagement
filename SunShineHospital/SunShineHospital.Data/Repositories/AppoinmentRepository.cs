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
    }
    public class AppoinmentRepository : RepositoryBase<Appoinment>, IAppoinmentRepository
    {
        public AppoinmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
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
    }
}
