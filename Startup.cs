using System;
using KhalidAbuhakmeh.AspNetCore.Search.Models.Elasticsearch;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Microsoft.Extensions.Hosting;


namespace KhalidAbuhakmeh.AspNetCore.Search
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
            var connectionString = Configuration.GetConnectionString("elasticsearch");

            // 1. Register NEST ElasticClient
            var settings = new ConnectionSettings(new Uri(connectionString))
                .DefaultIndex("capitals");

            services.AddSingleton(settings);

            services.AddScoped(s =>
            {
                var connectionSettings = s.GetRequiredService<ConnectionSettings>();
                var client = new ElasticClient(connectionSettings);

                return client;
            });


            // 2. Register Capital City Loader
            services.AddScoped<CapitalCities>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}