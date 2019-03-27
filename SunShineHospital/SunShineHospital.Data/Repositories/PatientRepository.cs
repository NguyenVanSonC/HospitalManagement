using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;
using System.Data.Entity;
using System.Linq;

namespace SunShineHospital.Data.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        int GetPatientIdByUserId(string userId);
    }

    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public int GetPatientIdByUserId(string userId)
        {
            var query = DbContext.Patients.Where(p => p.UserId == userId).Select(p => p.ID).SingleOrDefault();
            return query;
        }
    }
}