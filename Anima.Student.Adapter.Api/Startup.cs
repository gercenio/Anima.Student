using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Anima.Student.Application.Behaviors;
using Anima.Student.Infra.Data.Interfaces;
using Anima.Student.Infra.Data.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

namespace Anima.Student.Adapter.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Anima Digital Lab"
                    ,Description = "Anima Digital API REST criada com o ASP.NET Core"
                    ,Version = "0.0.1"
                });
                
                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });
            
            AddApplicationServices(services);
            
            services.AddControllers();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Anima Digital - Version 0.0.1");
            });
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICurriculumRepository, CurriculumRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ISchoolEnrollmentRepository, SchoolEnrollmentRepository>();
            
            services.AddLogging();
            
            AddMediatr(services);
            
        }
        
        private static string GetPathApplication()
        {
            return PlatformServices.Default.Application.ApplicationBasePath.ToString();
        }
        
        private static void AddMediatr(IServiceCollection services)
        {
            const string applicationAssemblyName = "Anima.Student.Application.dll";

            AssemblyName an = AssemblyName.GetAssemblyName(GetPathApplication()+applicationAssemblyName);
            
            var assembly = System.Reflection.Assembly.Load(an); 
            
            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            
            services.AddMediatR(assembly);
        }
    }
}