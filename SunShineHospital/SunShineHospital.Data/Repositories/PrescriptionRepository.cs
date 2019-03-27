using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Data.Repositories
{
    public interface IPrescriptionRepository : IRepository<Prescription>
    {
    }

    public class PrescriptionRepository : RepositoryBase<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}