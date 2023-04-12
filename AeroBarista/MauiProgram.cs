using AeroBarista.ApiClients;
using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Services;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels;
using AeroBarista.Views;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Java.Interop;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AeroBarista;

public static class MauiProgram
{
    private static Autofac.IContainer Container { get; set; }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureContainer(new AutofacServiceProviderFactory(), autofacBuilder =>
            {
                var assembly = Assembly.GetExecutingAssembly();

                Register(autofacBuilder, assembly);
                RegisterAs(autofacBuilder, assembly);

                RegisterApiClients(autofacBuilder);
            });


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static void Register(ContainerBuilder builder, Assembly assembly)
    {
        builder.RegisterAssemblyTypes(assembly)
        .Where(t => t.IsClass && t.GetCustomAttribute<ExportSingleton>() != null).SingleInstance();

        builder.RegisterAssemblyTypes(assembly)
        .Where(t => t.IsClass && t.GetCustomAttribute<ExportTransient>() != null).InstancePerDependency();

    }

    public static void RegisterAs(ContainerBuilder builder, Assembly assembly)
    {
        builder.RegisterAssemblyTypes(assembly)
         .Where(t => t.IsClass && t.GetCustomAttribute<ExportSingletonAs>() != null)
         .As(t => t.GetInterfaces().Where(x =>x.Name == t.GetCustomAttribute<ExportSingletonAs>().InterfaceName)).SingleInstance();

        builder.RegisterAssemblyTypes(assembly)
         .Where(t => t.IsClass && t.GetCustomAttribute<ExportTransientAs>() != null)
         .As(t => t.GetInterfaces().Where(x => x.Name == t.GetCustomAttribute<ExportTransientAs>().InterfaceName)).InstancePerDependency();
    }

    public static void RegisterApiClients(ContainerBuilder builder)
    {
        builder.RegisterType<DemoRecipeApiClient>().As<IRecipeApiClient>();
    }
}
