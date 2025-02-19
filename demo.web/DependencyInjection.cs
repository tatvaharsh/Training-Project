// using System.Reflection;
// using Microsoft.EntityFrameworkCore;
// using Demo.Entity.Data;

// namespace Demo.Web
// {
//     public static class DependencyInjection
//     {
//         public static void RegisterServices(IServiceCollection services, string connectionString)
//         {
//             services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));
//             var allReferencedTypes = Assembly
//                 .GetAssembly(typeof(DependencyInjection))
//                 .GetReferencedAssemblies()
//                 .Select(Assembly.Load)
//                 .SelectMany(a => a.GetTypes())
//                 .Where(t => t.Namespace != null &&
//                             (t.Namespace.StartsWith("Demo.Repository") || t.Namespace.StartsWith("Demo.Service")))
//                 .ToList();
//             var interfaces = allReferencedTypes.Where(t => t.IsInterface);

//             foreach (var serviceInterface in interfaces)
//             {
//                 var implementation = allReferencedTypes
//                     .FirstOrDefault(c =>
//                         c.IsClass &&
//                         !c.IsAbstract &&
//                         serviceInterface.Name.Substring(1) == c.Name);

//                 if (implementation != null)
//                 {
//                     services.AddScoped(serviceInterface, implementation);
//                 }
//             }
//         }

//     }
// }


using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Demo.Entity.Data;

namespace Demo.Web
{
    public static class DependencyInjection
    {
        public static void RegisterServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));

            RegisterImplementations(services, "Demo.Repository.Interfaces", "Demo.Repository.Implementations");
            RegisterImplementations(services, "Demo.Service.Interfaces", "Demo.Service.Implementations");
        }

        private static void RegisterImplementations(IServiceCollection services, string interfaceNamespace, string implementationNamespace)
        {
            var repositoryAssembly = Assembly.Load("Demo.Repository");
            var serviceAssembly = Assembly.Load("Demo.Service");

            var assemblies = new[] { repositoryAssembly, serviceAssembly };

            var interfaces = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsInterface && t.Namespace == interfaceNamespace);

            var implementations = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && t.Namespace == implementationNamespace);

            foreach (var serviceInterface in interfaces)
            {
                var implementation = implementations
                    .FirstOrDefault(c => serviceInterface.Name.Substring(1) == c.Name);
                if (implementation != null)
                {
                    services.AddScoped(serviceInterface, implementation);
                }
            }
        }
    }
}
