using SunShineHospital.Data.Infrastructure;
using SunShineHospital.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineHospital.Data.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {

    }
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
