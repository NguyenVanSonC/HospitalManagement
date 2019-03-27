using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Data.Repositories
{
    public interface ITestRepository : IRepository<Test>
    {
    }

    public class TestRepository : RepositoryBase<Test>, ITestRepository
    {
        public TestRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}