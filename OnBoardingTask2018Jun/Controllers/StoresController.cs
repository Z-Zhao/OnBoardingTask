using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
// using OnBoardingTask2018Jun.Models;
using OnBoardingTask2018Jun.Models.New_Models;

namespace OnBoardingTask2018Jun.Controllers
{
    public class StoresController : Controller
    {
        private CustomChangedEntities db = new CustomChangedEntities();

        // GET: Stores/Index (View)
        public ActionResult Index()
        {
            return View(db.Stores.ToList());
        }

        // GET: Stores/IndexTable (data of table on index page)
        public ActionResult IndexTable()
        {
            // sellect only needed property (get rid of Sold, etc.), to avoid circle reference when serialization
            var StoreIdNameAddress = db.Stores.ToList().Select(item => new { item.Id, item.Name, item.Address });

            // if not set JsonRequestBehavior.AllowGet, request will be blocked...
            // because sensitive information could be disclosed to third party web sites when this is used in a GET request.
            return Json(StoreIdNameAddress.ToList(), JsonRequestBehavior.AllowGet);
        }
        
        // GET: Stores/Create
        public ActionResult Create()
        { // no use in this case
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address")] Store store)
        {// data validation according to Model data annotation 
            if (ModelState.IsValid)
            { // data is valid
                db.Stores.Add(store);
                db.SaveChanges();
                return Json(new { Store = new { store.Id, store.Name, store.Address }, DataValid = true }, JsonRequestBehavior.AllowGet);
            }
            else
            { // data is not valid
                // Veryfy validation of each key 
                var ValidType = new
                {
                    IdValid = ModelState.IsValidField("Id"),
                    NameValid = ModelState.IsValidField("Name"),
                    AddressValid = ModelState.IsValidField("Address")
                };
                return Json(new { Store = new { store.Id, store.Name, store.Address }, DataValid = false, ValidType }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Stores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            
            // return only property which frontend needed, aviod ProductSolds,
            // otherwise it will cause circle reference
            return Json(new { store.Id,store.Name,store.Address }, JsonRequestBehavior.AllowGet);
            //return View(store); // return value to edit page (or edit modal)
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Store = new { store.Id, store.Name, store.Address }, DataValid = true }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            else // data is not valid
            {   // Veryfy validation of each key 
                var ValidType = new {
                    IdValid = ModelState.IsValidField("Id"),
                    NameValid = ModelState.IsValidField("Name"),
                    AddressValid = ModelState.IsValidField("Address")
                };
                // return only property which frontend needed, aviod ProductSolds,
                // otherwise it will cause circle reference
                return Json(new { Store= new { store.Id,store.Name,store.Address },DataValid=false, ValidType }, JsonRequestBehavior.AllowGet);
                //return View(store); // return this not valid data to original edit view
            }
            
        }

        // GET: Stores/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return Json(new { store.Id, store.Name, store.Address }, JsonRequestBehavior.AllowGet);
            //View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Store store = db.Stores.Find(id);
            if (store.ProductSolds.Count == 0) // store not used in productsolds
            {
                db.Stores.Remove(store);
                db.SaveChanges();
                return Json(new { Success = true }); // return success info to ajax post
            }
            else // store used in productsolds, can not be deleted because of foreign key constraint
            {
                return Json(new { Success = false });
                // return failed info to ajax post
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
