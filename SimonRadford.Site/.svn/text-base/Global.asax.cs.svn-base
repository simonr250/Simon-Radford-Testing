using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using MvcContrib.Castle;
using SimonRadford.Site.Classes;
using SimonRadford.Site.Controllers;


namespace SimonRadford.Site
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly IWindsorContainer _container = new WindsorContainer(new XmlInterpreter());

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Product", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            InitializeServiceLocator();

         //   NHibernateHelper.Configure(Server.MapPath("~/Hibernate.cfg.xml"));

            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }

        private void Global_Application_FirstBeginRequest(object sender, EventArgs e)
        {
        //    AreaRegistration.RegisterAllAreas(_container);
        }

        /// <summary>
        /// Instantiate the container and add all Controllers that derive from 
        /// WindsorController to the container.  Also associate the Controller 
        /// with the WindsorContainer ControllerFactory.
        /// </summary>
        private static void InitializeServiceLocator()
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_container));

            _container.RegisterControllers(typeof(ProductController).Assembly);
            ComponentRegistrar.AddComponentsTo(_container);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(_container));
        }
    }
}