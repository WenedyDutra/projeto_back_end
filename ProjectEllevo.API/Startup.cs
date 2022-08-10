using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectEllevo.API.Mappings;
using ProjectEllevo.API.Models;
using ProjectEllevo.API.Services;
using System.Text;

namespace ProjectEllevo.API
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
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            // requires using Microsoft.Extensions.Options
            services.Configure<mongoDbdatabaseSettings>(
                Configuration.GetSection(nameof(mongoDbdatabaseSettings)));

            services.AddSingleton<ImongoDbdatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<mongoDbdatabaseSettings>>().Value);

            //services.AddSingleton<UserAppService>();

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200");
                });
            });
            services.AddTransient<UserAppService>();
            services.AddTransient<TaskAppService>();
            services.AddTransient<ActivityAppService>();


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JwtKey").ToString())),
            //        ValidateIssuer =false,
            //        ValidateAudience = false
            //    };

            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer =false,
                    ValidateAudience = false
                };

            });
            services.AddAutoMapper(typeof(UserMapping));
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x=> x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
