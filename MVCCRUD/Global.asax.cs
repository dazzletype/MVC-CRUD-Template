using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCCRUD
{

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Sort deal by field
            routes.MapRoute(
                "SortDeals", // route name
                "SortBy/{sortby}", // URL matching pattern
                new { controller = "Home", action= "SortBy", sortBy = String.Empty}
            );


            // Filter deal by category
            routes.MapRoute(
              "FilterByCategory", 
              "FilterByCategory/{category}", 
              new { controller = "Home", action = "FilterByCategory", category = String.Empty } 
          );


            // Default fallthrough route
            routes.MapRoute(
                "Default", 
                "{controller}/{action}/{id}", 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } 
            );

        }

        protected void Application_Start()
        {

            
            //if (System.Configuration.ConfigurationManager.AppSettings["DropDatabaseOnChange"] == "1")
            //{
                //Set initializer to populate data on database creation;
                Database.SetInitializer(new EntitiesContextInitializer());
//            }

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}