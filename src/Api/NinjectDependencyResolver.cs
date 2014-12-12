using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;

namespace Api
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        protected IResolutionRoot Container;

        public NinjectDependencyResolver(IKernel container)
        {
            Container = container;
        }

        public NinjectDependencyResolver(IResolutionRoot container)
        {
            Container = container;
        }

        public object GetService(Type serviceType)
        {
            return Container.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            if (!(Container is IKernel))
            {
                throw new NotImplementedException("Can't begin a scope on a scope");
            }

            return new NinjectDependencyResolver(((IKernel) Container).BeginBlock());
        }

        public void Dispose()
        {
        }
    }
}