using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCoreAppFramework.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CorrelationId;
using Microsoft.Extensions.Logging;
using WebCoreAppFramework.Models;
using WebCoreAppFramework.Service;
using WebCoreAppFramework.Options;

namespace WebCoreAppFramework
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
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
            services.AddCorrelationId();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                 options => options.Stores.MaxLengthForKeys = 128)
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultUI(UIFramework.Bootstrap4)
                 .AddDefaultTokenProviders();

            services.Configure<AppSetupOptions>(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailService, EmailService>();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context,
                                  UserManager<ApplicationUser> userManager,
                                  RoleManager<ApplicationRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                _logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseCorrelationId(new CorrelationIdOptions
            {
                Header = "X-Correlation-ID",
                UseGuidForCorrelationId = true,
                UpdateTraceIdentifier = false
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            var options = new AppSetupOptions();
            Configuration.GetSection(nameof(AppSetupOptions)).Bind(options);

            SeedData.Initialize(context, userManager, roleManager, options, _logger).Wait();


        }
    }
}
