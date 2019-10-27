using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using DeviceSensorApi.Models;
using DeviceSensorApi.Services;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using DeviceSensorApi.Helpers;

namespace DeviceSensorApi
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
            services.Configure<DeviceDatabaseSettings>(
                Configuration.GetSection(nameof(DeviceDatabaseSettings)));

            services.AddSingleton<IDeviceDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DeviceDatabaseSettings>>().Value);

            services.AddSingleton<DeviceService>();

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v0",
                    Title = "Device Sensor Collection API",
                    Description = "API for registering devices and collecting their data.",
                    Contact = new OpenApiContact
                    {
                        Name = "Ben Cawrse",
                        Email = "bcawrse@gmail.com",
                        Url = new Uri("https://twitter.com/abenbot"),
                    }
                });

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    //OpenIdConnectUrl = new Uri("/api/authorize", UriKind.Relative),
                    Scheme = "basic"
                });

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    new OpenApiSecurityScheme()
                //    {
                        
                //    }
                //})

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "basic" },
                            Type = SecuritySchemeType.Http,
                            OpenIdConnectUrl = new Uri("/api/authenticate/", UriKind.Relative)
                        },
                        new[] { "readAccess", "writeAccess" }
                    }
                });

                //c.AddSecurityDefinition("basic", new OpenApiSecurityScheme()
                //{
                //    Type = SecuritySchemeType.Http,
                //    Scheme = "basic",
                //    author
                //});



                //c.DocumentFilter<BasicAuthenticationFilter>();

                // Define the Basic scheme that's in use (i.e. Implicit Flow)
                //c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.Http,
                //    Flows = new OpenApiOAuthFlows
                //    {
                //        ClientCredentials = new OpenApiOAuthFlow
                //        {
                //            AuthorizationUrl = new Uri("/api/authorize", UriKind.Relative),
                //            Scopes = new Dictionary<string, string>
                //            {
                //                { "readAccess", "Access read operations" },
                //                { "writeAccess", "Access write operations" }
                //            }
                //        },
                //        Implicit = new OpenApiOAuthFlow
                //        {
                //            AuthorizationUrl = new Uri("/api/authorize", UriKind.Relative),
                //            Scopes = new Dictionary<string, string>
                //            {
                //                { "readAccess", "Access read operations" },
                //                { "writeAccess", "Access write operations" }
                //            }
                //        }
                //    }
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                //        },
                //        new[] { "readAccess", "writeAccess" }
                //    }
                //});



                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Device Sensor API V0");
                c.RoutePrefix = string.Empty;
            });

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
