using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSD_ASP_NET_CORE_PROJECT.MiddleWare;
using Microsoft.AspNetCore.Builder;

namespace MCSD_ASP_NET_CORE_PROJECT.Extensions
{
    public static class GenisletmeMetotlari
    {
        public static IApplicationBuilder UseYazarEkleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<YazarEkleMiddleware>();

        }
    }
}
