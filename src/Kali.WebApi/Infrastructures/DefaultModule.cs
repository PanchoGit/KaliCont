using System.Linq;
using Autofac;
using Microsoft.Extensions.Configuration;
using Kali.Common.Helpers;
using Kali.Common.Logger;
using Kali.Common.Logger.Interfaces;

namespace Kali.WebApi.Infrastructures
{
    public class DefaultModule : Module
    {
        private const string ManagerAssemblyEndName = "Manager";
        private const string ServiceAssemblyEndName = "Service";
        private const string WorkflowAssemblyEndName = "Workflow";

        public IConfiguration Configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AssemblyHelper.GetReferencingAssemblies(nameof(Kali)).ToArray();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(s => s.Name.EndsWith(ManagerAssemblyEndName))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(s => s.Name.EndsWith(ServiceAssemblyEndName))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(s => s.Name.EndsWith(WorkflowAssemblyEndName))
                .AsImplementedInterfaces();

            RegisterLogger(builder);
        }

        private static void RegisterLogger(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(LoggerWrapper<>))
                .As(typeof(ILoggerWrapper<>));
        }
    }
}
