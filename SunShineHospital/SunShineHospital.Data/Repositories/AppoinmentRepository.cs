using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;

namespace SunShineHospital.Data.Repositories
{
    public interface IAppoinmentRepository : IRepository<Appoinment>
    {

    }
    public class AppoinmentRepository : RepositoryBase<Appoinment>, IAppoinmentRepository
    {
        public AppoinmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
