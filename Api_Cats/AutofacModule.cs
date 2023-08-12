using Autofac.Core;
using Autofac;
using Api_Cats.Entities;

namespace Api_Cats
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<Cat>().As<ICat>();

            //// Other Lifetime
            //// Transient
            //builder.RegisterType<MyService>().As<IService>()
            //    .InstancePerDependency();

            //// Scoped
            //builder.RegisterType<MyService>().As<IService>()
            //.InstancePerLifetimeScope();
            //builder.RegisterType<MyService>().As<IService>()
            //    .InstancePerRequest();

            //// Singleton
            //builder.RegisterType<MyService>().As<IService>()
            //    .SingleInstance();

            //// Scan an assembly for components
            ////builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
            ////       .Where(t => t.Name.EndsWith("Service"))
            ////       .AsImplementedInterfaces();
        }
    }
}
