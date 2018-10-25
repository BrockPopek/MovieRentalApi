using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MovieRental.DataAccess.DbContext;
using MovieRental.DataAccess.Entities;
using Swashbuckle.AspNetCore.Swagger;
using MovieRental.Contract.Service;
using MovieRental.DataAccess.Accessor;
using MovieRental.Service.Identity;
using MovieRental.Contract.DataAccess;
using MovieRental.Service.Customer;
using MovieRental.Service.Movie;

namespace MovieRentalApi
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
            services.AddDbContext<ApplicationUserDbContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationUserDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });

            // Add Authentication
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = signingKey,
                        ValidateAudience = true,
                        ValidAudience = this.Configuration["Tokens:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = this.Configuration["Tokens:Issuer"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });


            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new Info
                    {
                        Title = "Movie Rental API",
                        Version = "v1",
                        Description = @"Movie Rental API

To create a user to log into the API, go to the Register API below, and add the needed information and Role for the User.",
                        Contact = new Contact
                        {
                            Name = "Brock Popek",
                            Url = "https://github.com/BrockPopek"
                        }
                    }
                    );

                c.AddSecurityDefinition("Bearer", 
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please enter JWT Token with Bearer into Value field below. Example: \"Bearer {token}\"",
                        Name = "Authorization",
                        Type = "apiKey"              
                    });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer",  new string[] { } },
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Dependency Injection
            services.AddHttpContextAccessor();

            // DataAccess implementation
            services.AddTransient<IApplicationUserAccessor, ApplicationUserAccessor>();
            services.AddTransient<ICustomerAccessor, CustomerAccessor>();
            services.AddTransient<IMovieAccessor, MovieAccessor>();
            services.AddTransient<IRentalAccessor, RentalAccessor>();

            // Service implementation
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IRentalService, RentalService>();
            services.AddTransient<IPaymentService, PaymentService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
