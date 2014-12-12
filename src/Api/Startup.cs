using System.Web.Http;
using Api;
using Microsoft.Owin;
using Ninject;
using Ninject.Extensions.Conventions;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Api
{
    public class Startup
    {
        public static IKernel Container { get; private set; }

        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            Container = new StandardKernel();  
            Container.Bind(x => x.FromThisAssembly().SelectAllClasses().BindAllInterfaces());
            config.DependencyResolver = new NinjectDependencyResolver(Container);
     
            appBuilder.UseWebApi(config);
        }
    }
}