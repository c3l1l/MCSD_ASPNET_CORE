using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSD_ASP_NET_CORE_PROJECT.Extensions;
using MCSD_ASP_NET_CORE_PROJECT.MiddleWare;
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

            //app.Run(async context=>
            //{
            //    List<string> sehirler = new () {"Istanbul", "Izmir", "Bursa"};
            //    context.Response.WriteAsJsonAsync(sehirler);
            //});

            //Hata firlatma
            //app.Run(context =>
            //{
            //    throw new Exception("Hata olustu.");
            //});

            //app.UseWelcomePage();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseStaticFiles(); //Static sayflara erismemizi saglayan MiddleWare budur.
            // app.UseDirectoryBrowser();
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Merhaba Dünya!");
            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("MiddleWare#1");
            //});
            //Bu ara yazilim hic bir zaman calismaz....
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("MiddleWare#2");
            //});

            //app.Use(async (context, next) =>
            //{
            //    context.Response.ContentType = "text/html;charset=utf-8";
            //    await context.Response.WriteAsync("MiddleWare#1 Merhaba Düüünyyaýý");
            //    await next();
            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("<br/>MiddleWare#2");
            //});

            //app.Map("/HatBir", BirinciHat);
            //app.Map("/HatIki", IkinciHat);
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync($"Talep edilen yol {context.Request.Path}.");
            //});
            //app.Map("/Musteri", musteriApp =>
            //{
            //    musteriApp.Map("/Kaydet", musteriKaydeApp =>
            //    {
            //        musteriKaydeApp.Run(async context =>
            //        {
            //            await context.Response.WriteAsync($"Musteri.Kaydet");
            //        });
            //    });
            //});

            //app.MapWhen(context => context.Request.Query.ContainsKey("id"), ParametreGonderildi);

            //app.UseMiddleware<YazarEkleMiddleware>(); //Middleware olarak olusturulan siniftan middleware erisimi icin kullanildi.
            app.UseYazarEkleMiddleware(); //Burada olusturulan middleWare extension metot olarak eklendi ve diledigimiz yerde kullanilabilir.


            app.UseStaticFiles();
            app.Use(async (context, next) =>
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                await next();
            });
            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    await context.Response.WriteAsync($"Durum kodu:{context.Response.StatusCode}");
            //    await context.Response.WriteAsync($"Talep edilen yol:{context.Request.Path}");

            //});
            //app.Run(async context=>
            //{
            //  await  context.Response.WriteAsync("Anasayfaya Hosgeldiniz.");
            //});

            app.Use(async (context, next) =>
            {
                await next();
                string yol = context.Request.Path.Value;
                await context.Response.WriteAsync($"Talep edilen yol:{yol}");
                await context.Response.WriteAsync($"<br/>HTTP Status Code:{context.Response.StatusCode.ToString()}");
             


            });

            app.Map("/Hakkimizda", appHakkimizda =>
            {appHakkimizda.Run(async context =>
                {
                    context.Response.WriteAsync("Hakkimizda sayfasina hosgeldiniz.");
                });

            });
          
            app.MapWhen(context => context.Request.Path == "/", appAnasayfa =>
            {
                appAnasayfa.Run(async context =>
                {
                    context.Response.Redirect("/html/anasayfa.html");
                    await Task.FromResult(0);
                });
            });


        }

        Task MerhabaDunyaYaz(HttpContext context)
        {
            return context.Response.WriteAsync("Merhaba Dunya !");

        }

        void BirinciHat(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("1. hatcalisti !");
            });
        }

        void IkinciHat(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("2. hatcalisti !");
            });
        }
        void ParametreGonderildi(IApplicationBuilder app)
        {
            app.Run(
                async context =>
                {
                    var id = context.Request.Query["Id"];
                    await context.Response.WriteAsync($"Parametre Gönderildi. Deðeri: {id}");
                });
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
