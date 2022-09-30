using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Patrons.Data;
using Patrons.Samples.Factory;
using Patrons.Samples.Factory.Factories;
using Patrons.Samples.FactoryN;
using Patrons.Samples.FactoryN.Factories;

namespace Patrons
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            //builder.Services.AddTransient<ISample_001, Sample_001>();
            //builder.Services.AddSingleton<Func<ISample_001>>(x => () => x.GetService<ISample_001>());
            builder.Services.AddAbstractFactory<ISample_001, Sample_001>();
            builder.Services.AddAbstractFactory<ISample_002, Sample_002>();
            builder.Services.AddGenericClassWithDataFactory();

            builder.Services.AddVehiculeFactory();

            #region FactoryN    
            builder.Services.AddTransient<ISample_000, Sample_000>();

            //4
            builder.Services.AddTransient<ISample_000_001, Sample_000_001>();
            builder.Services.AddSingleton<Func<ISample_000_001>>(x => () => x.GetService<ISample_000_001>()!);
            
            //5
            builder.Services.NAddAbstractFactory<ISample_000_005, Sample_000_005>();
            builder.Services.NAddAbstractFactory<ISample_000_005_001, Sample_000_005_001>();

            //6
            builder.Services.NAddGenericClassWithDataFactory();

            //7
            builder.Services.NAddVehiculeFactory();


            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}