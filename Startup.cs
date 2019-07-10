using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ITDB.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ITDB.Tool;

namespace ITDB
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
              
            // services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            //使用huan'c
            services.AddMemoryCache();
            services.AddDbContext<DBContext>(opt => opt.UseMySql(Configuration.GetConnectionString("MySqlConnection")));//persist security info=True;user id=xd;password=fineex.com;MultipleActiveResultSets=True;
            services.AddCors();
            services.AddMvc(options =>
            {
                //options.Filters.Add(new CustomExceptionFilterAttribute(null, null)); // an instance
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;//关闭https
                // options.Audience = "http://localhost:5001/";
                // options.Authority = "http://localhost:5000/";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kj7ufh574rkj7ufh574rkj7ufh574rkj7ufh574rkj7ufh574rkj7ufh574rkj7ufh574rkj7ufh574r")),
                    ValidAudience = "audience",
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,//过期时间
                    ValidIssuer = "issuer"
                };
                // options.Configuration = new OpenIdConnectConfiguration(); 
            });  
            
            
            //启用https
            // services.Configure<MvcOptions>(options =>
            // {
            //     options.Filters.Add(new RequireHttpsAttribute());
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //http重定向https
            // var options = new RewriteOptions().AddRedirectToHttps();
            // app.UseRewriter(options);
            
            app.UseAuthentication();
            app.UseCors(builder =>builder.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials());
            // app.UseMvc();
            app.UseMvcWithDefaultRoute();
            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //         name: "default",
            //         template: "{controller=Home}/{action=Index}/{id?}");
            // });
            DbInitializer.Initialize(app.ApplicationServices.GetRequiredService<DBContext>());
        }
    }
}
