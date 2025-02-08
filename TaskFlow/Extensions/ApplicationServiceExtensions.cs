using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using TaskFlow.Application.UnitOfWork;
using TaskFlow.Extensions.SwaggerDocumentation;
using TaskFlow.Infrastracture.Connections;
using TaskFlow.Infrastracture.Repository.RepositoryUoW;

namespace TaskFlow.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Teste backend: Sistema de Gerenciamento de Tarefas",
                    Description = @"
                        Desenvolver um sistema de gerenciamento de tarefas utilizando C# ASP.NET MVC e Razor Pages. 
                        O sistema deve permitir que os usuários criem, editem, visualizem e excluam tarefas. 
                        Além disso, deve haver um recurso de autenticação para que cada usuário possa gerenciar suas próprias tarefas.:
                        - **Usuário**: usuário do sistema.
                        - **Task**: Atividade do usuário.
                        - **Categoria**: Categorias das atividades.                        

                        ### Base de Dados
                        - **PostgreSQL**

                        Para mais informações, consulte o repositório oficial:
                        [GitHub - TaskFlow](url do github).
                        ",
                });

                opt.OperationFilter<CustomOperationDescriptions>();
            }
            );

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseNpgsql(config.GetConnectionString("WebApiDatabase"));
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200");
                });
            });

            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
            services.AddScoped<IRepositoryUoW, RepositoryUoW>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition
                                   = JsonIgnoreCondition.WhenWritingNull;
            });

            return services;
        }
    }
}