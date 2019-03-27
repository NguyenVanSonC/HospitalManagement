using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Data.Repositories
{
    public interface IExaminationtRepository : IRepository<Examination>
    {
    }

    public class ExaminationRepository : RepositoryBase<Examination>, IExaminationtRepository
    {
        public ExaminationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}