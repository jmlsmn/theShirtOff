using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core.Resource;
using Castle.Windsor.Installer;
using Contest.Generic.Container;
using Contest.Generic.Models;
using Contest.Generic.Binders;
using Castle.Core.Logging;

namespace Contest.Generic
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private IWindsorContainer _container;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute(
                "{*favicon}", 
                new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" }
            );

            routes.MapRoute("Submission", "Submission/{submissionID}",
                new { controller = "Submissions", action = "Submission"});

            routes.MapRoute("Vote", "Vote/{submissionID}",
                new { controller = "Vote", action = "Vote" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders[typeof(UploadFileModel[])] = new UploadFilesBinder(); 

            _container = new WindsorContainer(new XmlInterpreter
                                            (new ConfigResource("castle")))
                                            .Install(FromAssembly.This());

            var controllerFactory = new CastleControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_End()
        {
            _container.Dispose();
        }
    }
}