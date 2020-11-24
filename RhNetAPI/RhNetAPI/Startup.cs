using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RhNetAPI.Contexts;
using RhNetAPI.Entities.Adm;

namespace RhNetAPI
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
            services.AddCors();

            services.AddDbContext<RhNetContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<RhNetContext>()
                .AddDefaultTokenProviders();
            
            services.AddScoped<RhNetContext, RhNetContext>();
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<RoleManager<ApplicationRole>>();

            services.AddControllers();

            var key = Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e");
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

            services.AddAuthorization(options =>
                {                   
                    options.AddPolicy("ViewMenu", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" || 
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Visualizar Menus")
                        ));
                    options.AddPolicy("UpdateMenu", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Atualizar Menus")
                        ));
                    options.AddPolicy("AddMenu", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Adicionar Menus")
                        ));
                    options.AddPolicy("RemoveMenu", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Remover Menus")
                        ));
                    options.AddPolicy("ViewPermission", policy => policy.RequireAssertion(context =>
                       context.User.Identity.Name == "master" ||
                       context.User.HasClaim(c => c.Type == "permission" && c.Value == "Visualizar Permissões")
                       ));
                    options.AddPolicy("UpdatePermission", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Atualizar Permissões")
                        ));
                    options.AddPolicy("AddPermission", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Adicionar Permissões")
                        ));
                    options.AddPolicy("RemovePermission", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Remover Permissões")
                        ));
                    options.AddPolicy("ViewClient", policy => policy.RequireAssertion(context =>
                      context.User.Identity.Name == "master" ||
                      context.User.HasClaim(c => c.Type == "permission" && c.Value == "Visualizar Clientes")
                      ));
                    options.AddPolicy("UpdateClient", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Atualizar Clientes")
                        ));
                    options.AddPolicy("AddClient", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Adicionar Clientes")
                        ));
                    options.AddPolicy("RemoveClient", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Remover Clientes")
                        ));
                    options.AddPolicy("ViewUser", policy => policy.RequireAssertion(context =>
                    context.User.Identity.Name == "master" ||
                    context.User.HasClaim(c => c.Type == "permission" && c.Value == "Visualizar Usuários")
                    ));
                    options.AddPolicy("UpdateUser", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Atualizar Usuários")
                        ));
                    options.AddPolicy("AddUser", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Adicionar Usuários")
                        ));
                    options.AddPolicy("RemoveUser", policy => policy.RequireAssertion(context =>
                        context.User.Identity.Name == "master" ||
                        context.User.HasClaim(c => c.Type == "permission" && c.Value == "Remover Usuários")
                        ));
                }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
