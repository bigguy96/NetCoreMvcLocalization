using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLocalizationTest
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddLocalization(options => options.ResourcesPath = "Resources");

            //services.AddMvc()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            //    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            //    .AddDataAnnotationsLocalization();

            services
                .AddLocalization(o => o.ResourcesPath = "Resources")
                .AddMvc(o => o.EnableEndpointRouting = false)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, o => o.ResourcesPath = "Resources")
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseCookiePolicy();

            //var supportedCultures = new[]
            //{
            //    new CultureInfo("en"),
            //    new CultureInfo("fr"),
            //};

            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("en"),
            //    // Formatting numbers, dates, etc.
            //    SupportedCultures = supportedCultures,
            //    // UI strings that we have localized.
            //    SupportedUICultures = supportedCultures
            //});

            //app.UseRequestLocalization(o =>
            //{
            //    o.SupportedCultures = o.SupportedUICultures = new[]
            //    {
            //        new CultureInfo("en"),
            //        new CultureInfo("fr")
            //    };

            //    o.DefaultRequestCulture = new RequestCulture(o.SupportedCultures[0]);
            //    o.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider() { Options = o });
            //});

            //app.UseMvcWithDefaultRoute();

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("fr"),
            };

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            var requestProvider = new RouteDataRequestCultureProvider();
            localizationOptions.RequestCultureProviders.Insert(0, requestProvider);
            app.UseRouter(routes =>
            {
                routes.MapMiddlewareRoute("{culture=en}/{*mvcRoute}", subApp =>
                {
                    subApp.UseRequestLocalization(localizationOptions);
                    subApp.UseMvc(mvcRoutes =>
                    {
                        mvcRoutes.MapRoute(
                            name: "default",
                            template: "{culture=en}/{controller=Home}/{action=Index}/{id?}");
                    });
                });
            });

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
