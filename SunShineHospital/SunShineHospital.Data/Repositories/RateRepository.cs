using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Data.Repositories
{
    public interface IRateRepository : IRepository<Rate>
    {
        Rate GetRateByUserId(string userId, int rateId);
    }

    public class RateRepository : RepositoryBase<Rate>, IRateRepository
    {
        public RateRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public Rate GetRateByUserId(string userId, int doctorId)
        {
            var query = from rate in DbContext.Rates
                where rate.DoctorID == doctorId && rate.UserId == userId
                select rate;
            return (Rate)query.SingleOrDefault();
        }
    }
}
