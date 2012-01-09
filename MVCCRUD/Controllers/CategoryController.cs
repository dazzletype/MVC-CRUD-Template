using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MVCCRUD.Controllers
{ 
    public class CategoryController : Controller
    {
        private InvoiceDB db = new InvoiceDB();

        //
        // GET: /Category/

        public ViewResult Index()
        {
            return View(db.Categories.ToList());
        }

        //
        // GET: /Category/Details/5

        public ViewResult Details(int id)
        {
            Category purchasetype = db.Categories.Find(id);
            return View(purchasetype);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Category purchasetype)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(purchasetype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(purchasetype);
        }
        
        //
        // GET: /Category/Edit/5
 
        public ActionResult Edit(int id)
        {
            Category purchasetype = db.Categories.Find(id);
            return View(purchasetype);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Category purchasetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchasetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchasetype);
        }

        //
        // GET: /Category/Delete/5
 
        public ActionResult Delete(int id)
        {
            Category purchasetype = db.Categories.Find(id);
            return View(purchasetype);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Category purchasetype = db.Categories.Find(id);
            db.Categories.Remove(purchasetype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}