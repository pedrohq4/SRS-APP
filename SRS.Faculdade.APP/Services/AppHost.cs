using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SRS.Faculdade.APP.Data;
using SRS.Faculdade.APP.Services;
using SRS.Faculdade.APP.View;

public static class AppHost
{
    public static ServiceProvider ServiceProvider { get; private set; }

    public static void Configure()
    {
        var services = new ServiceCollection();

        services.AddTransient<IUsuarioService, UsuarioService>();
        services.AddDbContext<UsuarioDbContext>(options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

        ServiceProvider = services.BuildServiceProvider();
    }
}
