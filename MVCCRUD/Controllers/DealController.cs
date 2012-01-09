using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using System.Globalization;

namespace MVCCRUD.Controllers
{
    [Authorize]
    public class DealController : Controller
    {
        private InvoiceDB db = new InvoiceDB();
        private int defaultPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultPaginationSize"]);

        /*CUSTOM*/

        public ViewResultBase Search(string text, string from, string to, int? page)
        {
            IQueryable<Deal> expenses = db.Deals;

            if (!string.IsNullOrWhiteSpace(from))
            {
                DateTime fromDate = DateTime.Parse(from, CultureInfo.CurrentUICulture);
                expenses = expenses.Where(t => t.CreatedDate >= fromDate);
            }
            if (!string.IsNullOrWhiteSpace(to))
            {
                DateTime toDate = DateTime.Parse(to, CultureInfo.CurrentUICulture);
                expenses = expenses.Where(t => t.CreatedDate <= toDate);
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                expenses = expenses.Where(t => t.Notes.ToLower().IndexOf(text.ToLower()) > -1);
            }

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var expensesListPaged = expenses.OrderByDescending(i => i.CreatedDate).ToPagedList(currentPageIndex, defaultPageSize);

            if (Request.IsAjaxRequest())
                return PartialView("Index", expensesListPaged);
            else
                return View("Index", expensesListPaged);
        }

        public PartialViewResult RecentPurchases(int? top)
        {
            if (!top.HasValue) top = 10;
            var invoices = db.Deals.Include(i => i.Provider).OrderByDescending(t=>t.CreatedDate).Take(top.Value);
            return PartialView("PurchasesListPartial", invoices.ToList());
        }

        public PartialViewResult RecentPurchasesByCustomer(int? providerID)
        {
            var invoices = db.Deals.Include(i => i.Provider).Where(p=>p.ProviderID==providerID).OrderByDescending(t => t.CreatedDate).Take(10);
            return PartialView("PurchasesListPartial", invoices.ToList());
        }
        /*END CUSTOM*/

        //
        // GET: /deal/

        public ViewResult Index(int? page)
        {
            var purchases = db.Deals.Include(p => p.Provider);
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            return View(purchases.OrderByDescending(p => p.CreatedDate).ToPagedList(currentPageIndex, defaultPageSize));
        }


        //
        // GET: /deal/Details/5

        public ViewResult Details(int id)
        {
            Deal deal = db.Deals.Find(id);
            return View(deal);
        }

        //
        // GET: /deal/Create

        public ActionResult Create()
        {
            Deal p = new Deal();
            p.CreatedDate = DateTime.Now;
            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "Name");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View(p);
        } 

        //
        // POST: /deal/Create

        [HttpPost]
        public ActionResult Create(Deal deal)
        {
            if (ModelState.IsValid)
            {
                db.Deals.Add(deal);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "Name", deal.ProviderID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View(deal);
        }
        
        //
        // GET: /deal/Edit/5
 
        public ActionResult Edit(int id)
        {
            Deal deal = db.Deals.Find(id);
            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "Name", deal.ProviderID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", deal.PurchaseTypeID);
            return View(deal);
        }

        //
        // POST: /deal/Edit/5

        [HttpPost]
        public ActionResult Edit(Deal deal, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/DealImages"), fileName);
                file.SaveAs(path);
            }

            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "Name", deal.ProviderID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", deal.PurchaseTypeID);
            return View(deal);
        }

        //
        // GET: /deal/Delete/5
 
        public ActionResult Delete(int id)
        {
            Deal deal = db.Deals.Find(id);
            return View(deal);
        }

        //
        // POST: /deal/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Deal deal = db.Deals.Find(id);
            db.Deals.Remove(deal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0) 
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/DealImages"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("EditOrCreateDealPartial");        
        }
       

    }
}