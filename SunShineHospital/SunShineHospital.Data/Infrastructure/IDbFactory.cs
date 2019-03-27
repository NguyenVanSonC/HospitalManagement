using SunShineHospital.Data;
using System;

namespace SunShineHospital.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        SunShineHospitalDbContext Init();
    }
}