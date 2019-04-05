using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Data.Repositories
{
    public interface IMedicineCategoryRepository : IRepository<MedicineCategory>
    {
    }

    public class MedicineCategoryRepository : RepositoryBase<MedicineCategory>, IMedicineCategoryRepository
    {
        public MedicineCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
