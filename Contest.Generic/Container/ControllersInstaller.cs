using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using System.Web.Mvc;
using Contest.Generic.Controllers;

namespace Contest.Generic.Container
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
           container.Register(AllTypes.FromThisAssembly()
                            .BasedOn<IController>()
                            .If(t => typeof(IController).IsAssignableFrom(t))
                            .Configure(c => c.LifeStyle.Transient));
        }
    }
}