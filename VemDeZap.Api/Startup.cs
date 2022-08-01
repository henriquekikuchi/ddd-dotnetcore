using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace VemDeZap.Api;

public class Startup
{
    public IConfiguration configRoot {
            get;
    }

    public Startup(IConfiguration configuration) {
        configRoot = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureMediatR();
        services.ConfigureRepositories();
        services.ConfigureSwagger();
        services.ConfigureAuthentication();
        services.ConfigureMVC();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // aceita requisições usando headers, metodos e origins
        app.UseCors(x => {
            x.AllowAnyHeader();
            x.AllowAnyMethod();
            x.AllowAnyOrigin();
        });

        // configura para usar mvc
        app.UseMvc();

        // documentação automatica da api
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "VemDeZap - V1");
            c.RoutePrefix = string.Empty;
        });
    }
}
