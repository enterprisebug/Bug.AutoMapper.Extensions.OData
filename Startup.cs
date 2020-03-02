using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData;
using OdataAutomapperServerQuery.Data;
using OdataAutomapperServerQuery.Profiles;

namespace OdataAutomapperServerQuery
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
            services.AddControllers(mvcOptions =>
                mvcOptions.EnableEndpointRouting = false);

            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyDbContext")));

            services.AddAutoMapper(Assembly.GetAssembly(typeof(ProjectReportToStandardProjectReportReadModelProfile)), Assembly.GetAssembly(typeof(MyDbContext)));

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
            });

            services.AddOData().EnableApiVersioning();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VersionedODataModelBuilder modelBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc(routeBuilder =>
            {
                // we need to set a new ODataConventionModelBuilder 
                // see https://github.com/Microsoft/aspnet-api-versioning/issues/476
                //var bla = modelBuilder.ModelBuilderFactory = () => new ODataConventionModelBuilder();

                var models = modelBuilder.GetEdmModels();

                routeBuilder.Select().Filter().Count().MaxTop(100);
                routeBuilder.ServiceProvider.GetRequiredService<ODataOptions>().UrlKeyDelimiter = ODataUrlKeyDelimiter.Parentheses;

                // odata must be called in the format: http://localhost:5103/odata/StandardProjectReport?api-version=1.0
                routeBuilder.MapVersionedODataRoutes("odata", "odata", models);
            });
        }
    }
}
