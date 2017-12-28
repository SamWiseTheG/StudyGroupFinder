using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudyGroupFinder.API.Utilities;
using StudyGroupFinder.Data;
using StudyGroupFinder.Data.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace StudyGroupFinder.API
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
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Study Group Finder API V1", Version = "v1" });
            });
            #endregion

            #region JWT Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ClockSkew = TimeSpan.Zero,

                            ValidIssuer = Configuration["JwtSecurity:Issuer"],
                            ValidAudience = Configuration["JwtSecurity:Audience"],
                            IssuerSigningKey = JwtHandler.CreateSigningKey(Configuration["JwtSecurity:SecretKey"])
                        };
                    });
            #endregion

            #region Dependency Injection
            services.AddTransient<DatabaseProvider>();
            services.AddTransient<UsersRepository>();
            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Study Group Finder API V1");
            });
            #endregion

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
