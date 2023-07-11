using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using MusicPlatform.Data;
using MusicPlatform.Data.Entities;
using MusicPlatform.DTO;
using MusicPlatform.Services;
using MusicPlatform.Services.Authentication;
using MusicPlatform.Services.Csv;
using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using MusicPlatform.Models;
using MusicPlatform.Services.Api;
using MusicPlatform.Services.Progress;
using Microsoft.AspNetCore.SignalR;

namespace MusicPlatform
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Add services to the DI container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
            builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
            builder.Services.AddTransient(typeof(ICsvService<>), typeof(CsvService<>));
            builder.Services.AddTransient<IApiService, ApiService>();
            builder.Services.AddScoped<IInitialDbService, InitialDbService>();
            builder.Services.AddSignalR();
            builder.Services.AddServerSideBlazor();
            builder.Services.Configure<FmApi>(FmApi => builder.Configuration
                                                              .GetSection("FmApi")
                                                              .Bind(FmApi));

            builder.Services.Configure<RecordsSettings>(settings => builder.Configuration
                                                                           .GetSection("Records")
                                                                           .Bind(settings));

            var config = new MapperConfiguration(
                                            cfg =>
                                            {
                                                cfg.CreateMap<Artist, ArtistDto>();


                                            }
                                           );
            var mapper = new Mapper(config);
            builder.Services.AddSingleton<IMapper>(mapper);
            builder.Logging.AddConsole();

            var configuration = builder.Configuration;
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(BuildConnectionString(configuration));


            });

            ObjectResolverService.SetServiceProvider(builder.Services?.BuildServiceProvider());
            var app = builder.Build();

            // If db does not exist, create it
            var dbContext = builder.Services.BuildServiceProvider().GetService<AppDbContext>();
            dbContext.Database.EnsureCreated();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapHub<ProgressHub>("/progressHub");
            });
            app.MapBlazorHub();
            app.Run();
        }


        private static string BuildConnectionString(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MusicConn");
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(connectionString);
            csb.InitialCatalog = "MusicDb";
            csb.TrustServerCertificate = true;


            return csb.ConnectionString;
        }

    }
}