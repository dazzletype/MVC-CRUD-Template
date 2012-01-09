using System;
using System.Web.Mvc;
using System.Linq;
using MvcPaging;

namespace MVCCRUD.Controllers
{
    public class HomeController : Controller
    {

        private InvoiceDB db = new InvoiceDB();
        private int defaultPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultPaginationSize"]);


        public ActionResult Index(int? page)
        {
            var deals = db.Deals;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            return View(deals.OrderByDescending(p => p.CreatedDate).ToPagedList(currentPageIndex, defaultPageSize));

        }


        public ActionResult SortBy(int? page, string sortBy)
        {
            var deals = db.Deals;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;


            switch (sortBy.ToLowerInvariant())
            {
                case "timeending": return View("Index", deals.OrderByDescending(p => p.ExpiryDate).ToPagedList(currentPageIndex, defaultPageSize));
                case "category": return View("Index", deals.OrderByDescending(p => p.Category.Name).ToPagedList(currentPageIndex, defaultPageSize));
                case "postdate": return View("Index", deals.OrderByDescending(p => p.CreatedDate).ToPagedList(currentPageIndex, defaultPageSize));
                default: return View("Index", deals.OrderByDescending(p => p.CreatedDate).ToPagedList(currentPageIndex, defaultPageSize));

            }
        }


        public ActionResult FilterByCategory(int? page, string category)
        {
            var deals = db.Deals;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            return View("Index", deals.OrderByDescending(p => p.CreatedDate).Where(d => d.Category.Name == category).ToPagedList(currentPageIndex, defaultPageSize));

        }


    }
}
