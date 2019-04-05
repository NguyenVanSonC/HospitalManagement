using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SunShineHospital.Areas.Admin
{
    public static class GlobalVariableApiHttp
    {
        public static HttpClient WebApiClient = new HttpClient();

        static GlobalVariableApiHttp()
        {
            WebApiClient.BaseAddress = new Uri("http://localhost:55364/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}