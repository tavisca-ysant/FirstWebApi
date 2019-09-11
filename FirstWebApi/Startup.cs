using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FirstWebApi.Service;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using FirstWebApi.Utility;
using NLog.Web;
using NLog.Extensions.Logging;


namespace FirstWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            env.ConfigureNLog("nlog.config");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<IBookService, BookService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddNLog();
            app.AddNLogWeb();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("middleware 2");

            //});
            app.UseMiddleware<LoggingMiddleware>();
            //app.Use(async (context, next) =>
            //{
            //    await MiddleWare(context, next);
            //});
        //    app.UseMiddleware<ValidationMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        //public async Task MiddleWare(HttpContext httpContext, Func<Task> next)
        //{
        //    var httpContext1 = httpContext;
        //    var requestMethod = httpContext1.Request.Method;
        //    if (requestMethod.Equals("POST"))
        //    {
                
        //    }
        //    await next();
        //}
    }
}
