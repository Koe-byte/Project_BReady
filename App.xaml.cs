using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ProjectBReadyWPF.Backend.Interfaces;
using ProjectBReadyWPF.Backend.Services;

namespace ProjectBReadyWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
    public static ServiceProvider ServiceProvider { get; private set; }
#pragma warning restore CS8618 

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        // Register Services
        services.AddSingleton<IShelterService, ShelterService>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IDashboardService, DashboardService>();
        services.AddSingleton<IDispatchService, DispatchService>();
        services.AddSingleton<IInventoryService, InventoryService>();
    }
}

