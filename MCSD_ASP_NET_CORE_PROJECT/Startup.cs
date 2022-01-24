using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSD_ASPNET_CORE;
using Microsoft.AspNetCore.Http;

namespace MCSD_ASP_NET_CORE_PROJECT
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            //var kullanici = new Kullanici();
            //var sd = new ServiceDescriptor(typeof(IKullanici), kullanici);
            //services.Add(sd);

           // services.Add(new ServiceDescriptor(typeof(IKullanici),new Kullanici()));
            services.Add(new ServiceDescriptor(typeof(IMotor),new BenzinMotor())); //Singleton
            services.Add(new ServiceDescriptor(typeof(IKullanici),typeof(Kullanici), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IKullanici),typeof(Kullanici),ServiceLifetime.Scoped));
            services.AddSingleton(typeof(IKullanici),typeof(Kullanici)); //I liked it so much !!!
            services.AddSingleton<IKullanici, Kullanici>(); //I liked it !!!

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});

            //app.Run(async context => {
            //        context.Response.StatusCode = 404; 
            //        await context.Response.WriteAsync("Merhaba Dunya !");
            //    });
            
            //app.Run(MerhabaDunyaYaz);
            app.Run(async context=>
            {
                List<string> sehirler = new () {"Istanbul", "Izmir", "Bursa"};
                context.Response.WriteAsJsonAsync(sehirler);
            });
        }

        Task MerhabaDunyaYaz(HttpContext context)
        {
            return context.Response.WriteAsync("Merhaba Dunya !");

        }
    }
    public interface IKullanici
    {
        void Bilgi() { }
    }

    public class Kullanici : IKullanici
    {
        void Bilgi()
        {
            Console.WriteLine("Kullanýcýnýn Bilgileri");
        }
    }
}
