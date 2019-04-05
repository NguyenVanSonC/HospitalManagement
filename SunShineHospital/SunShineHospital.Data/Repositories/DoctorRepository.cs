using System.Collections.Generic;
using System.Data.Entity;
using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;
using System.Linq;

namespace SunShineHospital.Data.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        ApplicationUser GetUserByDoctorId(int id);

        Doctor GetDoctorIdByUserId(string userId);
    }

    public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {
        public DoctorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Doctor GetDoctorIdByUserId(string userId)
        {
            var query = DbContext.Doctors.Where(p => p.UserId == userId).Select(p => p).SingleOrDefault();
            return query;
        }

        public ApplicationUser GetUserByDoctorId(int id)
        {
            var query = from d in DbContext.Doctors
                join u in DbContext.Users
                    on d.UserId equals u.Id
                select u;
            return (ApplicationUser)query;
        }
    }
}