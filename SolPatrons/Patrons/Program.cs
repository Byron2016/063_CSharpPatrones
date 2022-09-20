using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Patrons.Data;
using Patrons.Samples.Factory;
using Patrons.Samples.Factory.Factories;

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