using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            this._config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // 여기 서비스에서의 선언 순서는 크게 중요하지 않음. 하지만 아래 Configure의 경우는 middleware이고 이거 같은경우는 위에서 부터 아래로 실행되므로 순서가 중요함.
        public void ConfigureServices(IServiceCollection services)
        {



            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<StoreContext>(options =>
            {
                // options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
                options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
                // options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));

            });

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlite(_config.GetConnectionString("IdentityConnection"));
            });

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddApplicationServices();

            services.AddIdentityServices(_config);

            services.AddSwaggerDocumentation();

            services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"); ;
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // 여기 Configure의 경우는 middleware이고 이거 같은경우는 위에서 부터 아래로 실행되므로 순서가 중요함.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {



            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwaggerDocumentation();

            // if (env.IsDevelopment())
            // {
            //     // app.UseDeveloperExceptionPage(); // 이 부분이 원래 exception handling middleware를 동작시키는 부분
            // }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles(); // 위치는 app.UseRouting(); 아래에 위치. staticFile을 불러올 수 있게 해주는 설정 값

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
