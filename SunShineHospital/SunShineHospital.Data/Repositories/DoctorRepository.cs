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
    }

    public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {
        public DoctorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
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