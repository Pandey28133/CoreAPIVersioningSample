using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIVersioning
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
            services.AddControllers();
            services.AddApiVersioning(x=> {
                x.AssumeDefaultVersionWhenUnspecified = true;
                // default Mediatype Accept pass "version" from request header and this will overridde default url API versioning
                // for example:-   accept = application/json; version=2.0

                //x.ApiVersionReader = new MediaTypeApiVersionReader("version");

                // custome header name for api version, pass "x-version" from request header and this will overridde default url API versioning
                // for example;-  x-version = 2.0

                x.ApiVersionReader = new HeaderApiVersionReader("x-version");

                // to support Mediatype Accept and custom header name 
                // default Mediatype Accept pass "version" from request header and this will overridde default url API versioning
                // custome header name for api version, pass "x-version" from request header and this will overridde default url API versioning
                // for example:-   accept = application/json; version=2.0 or x-version = 2.0                 

                //x.ApiVersionReader = new ApiVersionReader.Combine(
                //    new MediaTypeApiVersionReader("version"),
                //    new HeaderApiVersionReader("x-version")
                //    );                

                // response header api-supported-versions: 1.0, 2.0 to let the clinet know about the versions of API avaialbity
                x.ReportApiVersions = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
