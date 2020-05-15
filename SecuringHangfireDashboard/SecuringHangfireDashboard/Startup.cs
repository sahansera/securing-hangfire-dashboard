using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace SecuringHangfireDashboard
{
    public class Startup
    {
        private const string HangfirePolicyName = "HangfirePolicy"; // Can be any name

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options => Configuration.Bind("AzureAd", options));

            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            {
                options.Authority = options.Authority + "/v2.0/";         // Microsoft identity platform

                options.TokenValidationParameters.ValidateIssuer = false; // accept several tenants (here simplified)
            });

            // Add a new policy for hangfire
            services.AddAuthorization(options =>
            {
                // Policy to be applied to hangfire endpoint
                options.AddPolicy(HangfirePolicyName, builder =>
                {
                    builder
                        .AddAuthenticationSchemes(AzureADDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser();
                });
            });
            services.AddControllers();
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // Hangfire Settings
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard("/hangfire", new DashboardOptions()
                {
                    Authorization = new List<IDashboardAuthorizationFilter> { }
                })
                .RequireAuthorization(HangfirePolicyName);
            });

            //Register our background job
            RecurringJob.AddOrUpdate("some-id", () => Console.WriteLine(), Cron.Minutely);
        }
    }
}
