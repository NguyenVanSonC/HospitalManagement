using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Data.Repositories
{
    public interface IMedicineRepository : IRepository<Medicine>
    {
    }

    public class MedicineRepository : RepositoryBase<Medicine>, IMedicineRepository
    {
        public MedicineRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}