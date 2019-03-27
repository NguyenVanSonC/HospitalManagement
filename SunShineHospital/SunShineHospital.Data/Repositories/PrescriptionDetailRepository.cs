using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Data.Repositories
{
    public interface IPrescriptionDetailRepository : IRepository<PrescriptionDetail>
    {
    }

    public class PrescriptionDetailRepository : RepositoryBase<PrescriptionDetail>, IPrescriptionDetailRepository
    {
        public PrescriptionDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
