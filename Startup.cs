using EmployeeFormApi.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;



namespace EmployeeFormApi
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
                // Adding CORS policy to allow frontend to call the API
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin",
                        builder => builder
                            .WithOrigins("https://localhost:44318")  // The frontend URL (Adjust if different)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());  // Allow credentials (cookies or headers)
                });

                // Configuring the DbContext for SQL Server using the connection string from appsettings.json
                services.AddDbContext<EmployeeContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

                // Adding MVC controllers to the service container
                services.AddControllers();

                // Adding Swagger for API documentation
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeFormApi", Version = "v1" });
                });
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeFormApi v1"));
                }

                // Enabling HTTPS Redirection
                app.UseHttpsRedirection();

                // Enabling Routing
                app.UseRouting();

                // Applying CORS policy to allow cross-origin requests
                app.UseCors("AllowSpecificOrigin");

                // Applying authorization (if needed)
                app.UseAuthorization();

                // Setting up endpoints for controllers
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }
