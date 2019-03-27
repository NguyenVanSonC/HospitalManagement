using SunShineHospital.Data;

namespace SunShineHospital.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private SunShineHospitalDbContext dbContext;

        public SunShineHospitalDbContext Init()
        {
            return dbContext ?? (dbContext = new SunShineHospitalDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}