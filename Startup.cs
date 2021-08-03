using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;

namespace ElectricalShop
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(op =>
            {
                op.LoginPath = new PathString("/auth/signin");
                op.LogoutPath = new PathString("/auth/signout");
                op.ExpireTimeSpan = TimeSpan.FromDays(30);
                op.AccessDeniedPath = new PathString("/auth/denied");
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            // Using MVC model
            // basic template has 3 parts: controller, action and id
            app.UseMvc(r => r.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"));


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
