using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Data.Repositories
{
    public interface IExaminationDetailRepository : IRepository<ExaminationDetail>
    {
    }

    public class ExaminationDetailRepository : RepositoryBase<ExaminationDetail>, IExaminationDetailRepository
    {
        public ExaminationDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}