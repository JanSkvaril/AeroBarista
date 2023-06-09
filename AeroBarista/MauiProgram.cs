using AeroBarista.ApiClients;
using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Database;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Camera.MAUI;
using Microsoft.Extensions.Logging;
using System.Reflection;
using CommunityToolkit.Maui;

namespace AeroBarista;

public static class MauiProgram
{
    private static Autofac.IContainer Container { get; set; }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCameraView()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureContainer(new AutofacServiceProviderFactory(), autofacBuilder =>
            {
                var assembly = AppDomain.CurrentDomain.GetAssemblies();

                Register(autofacBuilder, assembly);
                RegisterAs(autofacBuilder, assembly);

                RegisterApiClients(autofacBuilder, assembly, true);
            });


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static void Register(ContainerBuilder builder, Assembly[] assembly)
    {
        builder.RegisterAssemblyTypes(assembly)
        .Where(t => t.IsClass && t.GetCustomAttribute<ExportSingleton>() != null).SingleInstance();

        builder.RegisterAssemblyTypes(assembly)
        .Where(t => t.IsClass && t.GetCustomAttribute<ExportTransient>() != null).InstancePerDependency();

    }

    public static void RegisterAs(ContainerBuilder builder, Assembly[] assembly)
    {
        builder.RegisterAssemblyTypes(assembly)
         .Where(t => t.IsClass && t.GetCustomAttribute<ExportSingletonAs>() != null)
         .As(t => t.GetInterfaces().Where(x =>x.Name == t.GetCustomAttribute<ExportSingletonAs>().InterfaceName)).SingleInstance();

        builder.RegisterAssemblyTypes(assembly)
         .Where(t => t.IsClass && t.GetCustomAttribute<ExportTransientAs>() != null)
         .As(t => t.GetInterfaces().Where(x => x.Name == t.GetCustomAttribute<ExportTransientAs>().InterfaceName)).InstancePerDependency();
    }

    public static void RegisterApiClients(ContainerBuilder builder, Assembly[] assembly, bool useDemoApiClients = false)
    {
        builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.Name.EndsWith("ApiClient") && 
                (useDemoApiClients ? t.Name.StartsWith("Demo") : !t.Name.StartsWith("Demo")))
            .AsImplementedInterfaces()
            .InstancePerDependency();

        if(useDemoApiClients)
            builder.RegisterType<DemoDatabase>().SingleInstance();
    }
}
