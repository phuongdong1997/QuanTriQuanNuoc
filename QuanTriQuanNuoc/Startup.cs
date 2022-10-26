using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QuanTriQuanNuoc.Data;
using QuanTriQuanNuoc.Entites;
using QuanTriQuanNuoc.Identity;
using QuanTriQuanNuoc.Models;
using QuanTriQuanNuoc.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanTriQuanNuoc
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
            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));
            //2. Setup idetntity
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryApiResources(Config.Apis)
            .AddInMemoryClients(Config.Clients)
            .AddInMemoryIdentityResources(Config.Ids)
            .AddAspNetIdentity<User>()
            .AddDeveloperSigningCredential();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            });

            services.AddControllersWithViews()
                 .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CategoryCreateRequestValidator>());
            services.AddRazorPages(options =>
            {
                options.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account/", model =>
                {
                    foreach (var selector in model.Selectors)
                    {
                        var attributeRouteModel = selector.AttributeRouteModel;
                        attributeRouteModel.Order = -1;
                        attributeRouteModel.Template = attributeRouteModel.Template.Remove(0, "Identity".Length);
                    }
                });
            });
            services.AddTransient<DbInitializer>();
            services.AddTransient<IEmailSender, EmailSenderService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Knowledge Space API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId("swagger");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuanTriHangHoa API V1");
            });
        }
    }
}