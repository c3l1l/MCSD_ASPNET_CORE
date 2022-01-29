using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MCSD_ASP_NET_CORE_PROJECT.MiddleWare
{
    public class YazarEkleMiddleware
    {
        private RequestDelegate _next;
        public YazarEkleMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("Yazar","Celil Ozturk");
            await _next(context);
        }
    }
}
