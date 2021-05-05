using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IDataAccess;
using DataAccess;
using IBusinessLogic;
using BusinessLogic;
using Domain;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContext<Context>(
                o => o.UseSqlServer(Configuration.GetConnectionString("BetterCalmDB"))
            );
            //registro repositorio y logica para admin
            services.AddScoped<IAdministratorRepository<Administrator>, AdministratorRepository>();
            services.AddScoped<IAdministratorBL, AdministratorBL>();
            //registro repositorios y logica para playablecontent
            services.AddScoped<IRepository<Playlist>, PlaylistRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Problem>, ProblemRepository>();
            services.AddScoped<IRepository<PlayableContent>, PlayableContentRepository>();
            services.AddScoped<IPlayerBL, PlayerBL>();
            services.AddScoped<IRepository<Psychologist>, PsychologistRepository>();
            services.AddScoped<IRepository<Schedule>, ScheduleRepository>();
            services.AddScoped<IPsychologistBL, PsychologistBL>();
            services.AddScoped<IRepository<Consultation>, ConsultationRepository>();
            services.AddScoped<IConsultationBL, ConsultationBL>();
            //esta parte es la de autenticacion
            var key = Encoding.ASCII.GetBytes("Ahora este es nuestro secreto.");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();

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
