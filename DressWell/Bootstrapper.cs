using DressWell.Business.Interfaces;
using DressWell.Data.Interfaces;
using DressWell.Presentation.Interfaces;
using DressWell.Data.Repository;
using DressWell.Business.Services;
using DressWell.Presentation.Services;
using Ninject;
using System;

namespace DressWell
{
    public class Bootstrapper
    {
        public static IKernel Init()
        {
            var container = new StandardKernel();
            RegisterTypes(container);
           
            return container;
        }

        private static void RegisterTypes(IKernel container)
        {
            container.Bind<IDressWellRepository>().To<DressWellRepository>().InSingletonScope();

            container.Bind<IDressWellBusinessService>().To<DressWellBusinessService>()
                .InSingletonScope()
                .WithConstructorArgument<IDressWellRepository>(container.Get<DressWellRepository>());

            container.Bind<IDressWellPresentationServices>().To<DressWellPresentationServices>()
                .InSingletonScope()
                .WithConstructorArgument<IDressWellBusinessService>(container.Get<DressWellBusinessService>());
        }
    }
}
