using API_Citel.Application;
using Dominio.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace API_Citel
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            #region [ CONEXÃO BANCO DE DADOS ]

            services.AddDbContext<DataModel>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseProd")));
            services.AddScoped<DataModel, DataModel>();

            #endregion

            //Injeção de dependência
            services.AddTransient<CategoriaApplication>();
            services.AddTransient<ProdutoApplication>();

            #region [ CONFIGURAÇÕES SWAGGER ]

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                  new OpenApiInfo
                  {
                      Title = "API Citel",
                      Version = "v1",
                      Description = "Micro serviço para integração com o Aplicativo API Citel"
                  });
            });

            #endregion

            services.AddControllers();

            //Permitir retorno de JSON maiores do que o padrão Net Core 3.0
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
#if DEBUG
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Citel");
#else
                c.SwaggerEndpoint("/Produto_Categoria/swagger/v1/swagger.json", "API Citel");
#endif
            });
        }
    }
}
