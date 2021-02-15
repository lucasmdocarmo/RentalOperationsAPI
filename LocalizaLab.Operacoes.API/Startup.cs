using LocalizaLab.Operacoes.API.Extensions;
using LocalizaLab.Operacoes.Application.Command;
using LocalizaLab.Operacoes.Application.Command.Autenticacao;
using LocalizaLab.Operacoes.Application.Command.Carros;
using LocalizaLab.Operacoes.Application.Command.Carros.Veiculo;
using LocalizaLab.Operacoes.Application.Command.Contratos;
using LocalizaLab.Operacoes.Application.Command.Marca;
using LocalizaLab.Operacoes.Application.Command.Modelos;
using LocalizaLab.Operacoes.Application.Command.Reservas;
using LocalizaLab.Operacoes.Application.Command.Usuario;
using LocalizaLab.Operacoes.Application.Handlers;
using LocalizaLab.Operacoes.Application.Presentation;
using LocalizaLab.Operacoes.Application.Queries;
using LocalizaLab.Operacoes.Application.Queries.Base;
using LocalizaLab.Operacoes.Application.Queries.Models;
using LocalizaLab.Operacoes.Application.Queries.Query;
using LocalizaLab.Operacoes.Application.Queries.Reservas;
using LocalizaLab.Operacoes.Application.Queries.Veiculos;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
using LocalizaLab.Operacoes.Domain.Contracts.Repository;
using LocalizaLab.Operacoes.Domain.Queries;
using LocalizaLab.Operacoes.Domain.Shared.Contracts;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using LocalizaLab.Operacoes.Infra.Context;
using LocalizaLab.Operacoes.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace LocalizaLab.Operacoes.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public IUnitOfWork _unitOfWork { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));

            }).AddJsonOptions(json => { json.JsonSerializerOptions.IgnoreNullValues = true; }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddCors();
            services.AddControllers();

            services.AddDbContext<OperacoesContext>(db => db.UseSqlServer(Configuration.GetConnectionString("AppOperacoesCatalog")));
            services.AddScoped<OperacoesContext>();
            services.AddScoped<IUnitOfWork, OperacoesContext>();

            services.AddSwaggerConfig();
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(typeof(ApplicationMappingProfile));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Operador", policy => policy.RequireClaim("Operador",  ETipoPerfil.Cliente.ToString()));
                options.AddPolicy("Cliente", policy => policy.RequireClaim("Cliente", ETipoPerfil.Operador.ToString()));
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            _unitOfWork = services.BuildServiceProvider().GetRequiredService<IUnitOfWork>();

            //Application
            services.AddScoped<ICommandResult, CommandResult>();
            services.AddScoped<ICommandHandler<CadastrarVeiculoCommand>, CarrosHandler>();
            services.AddScoped<ICommandHandler<CadastrarMarcaCommand>, CarrosHandler>();
            services.AddScoped<ICommandHandler<CadastrarModeloCommand>, CarrosHandler>();
            services.AddScoped<ICommandHandler<CadastrarOperadorCommand>, UsuarioHandler>();
            services.AddScoped<ICommandHandler<CadastrarUsuarioCommand>, UsuarioHandler>();
            services.AddScoped<ICommandHandler<CadastrarEnderecoCommand>, UsuarioHandler>();
            services.AddScoped<ICommandHandler<CadastrarClienteCommand>, UsuarioHandler>();
            services.AddScoped<ICommandHandler<AutenticarUsuarioCommand>, AutenticacaoHandler>();
            services.AddScoped<ICommandHandler<DeletarVeiculoCommand>, CarrosHandler>();
            services.AddScoped<ICommandHandler<EditarVeiculoCommand>, CarrosHandler>();
            services.AddScoped<ICommandHandler<SimularReservaCommand>, ReservaHandler>();
            services.AddScoped<ICommandHandler<CadastrarContratoCommand>, ContratoHandler>();
            services.AddScoped<ICommandHandler<DevolverContratoCommand>, ContratoHandler>();
            services.AddScoped<ICommandHandler<PagarContratoCommand>, ContratoHandler>();
            services.AddScoped<ICommandHandler<CadastrarReservaCommand>, ReservaHandler>();
            services.AddScoped<ICommandHandler<DeletarReservaCommand>, ReservaHandler>();
            services.AddScoped<ICommandHandler<AgendarVeiculoCommand>, CarrosHandler>();
            services.AddScoped<ICommandHandler<BaixarContratoCommand>, ContratoHandler>();

            //Queries
            services.AddScoped<IQueryResult, QueryResult>();
            services.AddScoped<IQueryHandler<UsuarioQuery>, UsuarioQueries>();
            services.AddScoped<IQueryHandler<VeiculoQuery>, VeiculoQueries>();
            services.AddScoped<IQueryHandler<TodosVeiculosQuery>, VeiculoQueries>();
            services.AddScoped<IQueryHandler<ConsultarReservaQuery>, ReservaQueries>();

            //Repos
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IContratoRepository, ContratoRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IModeloRepository, ModeloRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IOperadorRepository, OperadorRepository>();
            services.AddScoped<IAgendamentosRepository, AgendamentosRepository>();
            services.AddScoped<IDadosItemContratoRepository, DadosItemContratoRepository>();
            services.AddScoped<IDadosContratoDevolucaoRepository, DevolucaoContratoRepository>();
            services.AddScoped<IDadosPagamentosRepository, DadosPagamentoRepository>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, OperacoesContext appContext)
        {
            app.UsePathBase("/LocalizaLabs-Operations");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (!_unitOfWork.CheckDatabaseStatus())
            {
                appContext.Database.EnsureCreated();
            }
            else
            {
                appContext.Database.Migrate();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "LL.Operations.Rental"); });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHsts();
            // app.UseHealthChecks("/Health");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
       
       
    }
}
