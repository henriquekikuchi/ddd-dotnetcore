using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using VemDeZap.Api.Security;
using VemDeZap.Domain.Commands.Usuario.AdicionarUsuario;
using VemDeZap.Domain.Interfaces.Repositories;
using VemDeZap.Infra.Repositories;
using VemDeZap.Infra.Repositories.Base;
using VemDeZap.Infra.Repositories.Transactions;

namespace VemDeZap.Api;

public static class Setup
{
    private const string ISSUER =  "c1f51f42";
    private const string AUDIENCE = "c6bbbb645024";

    public static void ConfigureAuthentication(this IServiceCollection services)
    {
        //Configurações de token
        var signingConfigurations = new SigningConfigurations();
        services.AddSingleton(signingConfigurations);

        var tokenConfigurations = new TokenConfigurations
        {
            Audience = AUDIENCE,
            Issuer = ISSUER,
            Seconds = int.Parse(TimeSpan.FromDays(1).TotalSeconds.ToString())
        };
        services.AddSingleton(tokenConfigurations);

        services.AddAuthentication(authOptions => 
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions => 
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.SigningCredentials.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                //valida a assinatura do token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é valido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerancia para a expiração de um token (utilizando
                // caso haja problemas de sincronismo de horario entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;

                // Ativa o uso do token como forma de autorizar o acesso
                // a recursos deste projeto
                services.AddAuthorization(auth => 
                {
                    auth.AddPolicy("Bearer", new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
                });

                // Para todas as requisições serem necessario o token, para um endpoint não exigir o token
                // deve colocar o [AllowAnnonymous]
                // caso remova essa linha, para todas as requisições que precisar de token, deve colocar
                // o atributo [Authorize("Bearer")]
                services.AddMvc(config => 
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser().Build();

                    config.Filters.Add(new AuthorizeFilter(policy));
            });
        });

        services.AddCors();
    }

    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly, typeof(AdicionarUsuarioRequest).GetTypeInfo().Assembly);
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddSingleton<VemDeZapContext, VemDeZapContext>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        
        //repositories
        services.AddTransient<IRepositoryUsuario, RepositoryUsuario>();
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c => 
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "VemDeZap.Portal", Version = "v1" });
        });
    }

    public static void ConfigureMVC(this IServiceCollection services)
    {
        // services.AddControllers();
        services.AddControllers();
        services.AddMvc(options =>
        {
            //var policy = new AuthorizationPolicyBuilder()
            //    .RequireAuthenticatedUser()
            //    .Build();
            //options.Filters.Add(new AuthorizeFilter(policy));
            options.EnableEndpointRouting=false;
        })
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }
}
