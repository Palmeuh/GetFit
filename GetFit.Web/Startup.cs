using GetFit.Domain.Models;
using GetFit.Infrastructure;
using GetFit.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GetFit.Web
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
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();


            services.AddDbContext<GetFitContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("GetFitContext")));


            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<GetFitContext>();

            services.AddScoped<IRepository<Excercise>, ExcerciseRepository>();
            services.AddScoped<IRepository<Workout>, WorkoutRepository>();
            services.AddScoped<IRepository<WorkoutProgram>, WorkoutProgramRepository>();
            services.AddScoped<IRepository<WorkoutPlan>, WorkoutPlanRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                GetFitSeedData.CreateInitialDatabaseAsync(app).Wait(); //need to move this as it can create a deadlock
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });



        }


    }
}
