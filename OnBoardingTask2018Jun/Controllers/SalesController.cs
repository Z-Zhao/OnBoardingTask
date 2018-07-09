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
    public class SalesController : Controller
    {
        private CustomChangedEntities db = new CustomChangedEntities();

        // GET: Sales/Index (View)
        public ActionResult Index()
        {
            return View();
        }

        // GET: Sales/IndexTable (get data-SalesToList for table on index page)
        public ActionResult IndexTable()
        {
            // datatype: List<{ DateSold, Customer, Product, Store, Id, ProductId, CustomerId, StoreId}>
            var productSolds = db.ProductSolds.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Store).ToList();
            // sellect only needed property, to avoid circle reference when serialization
            var SalesToList = productSolds.Select(item => new {
                Id = item.Id,
                //To output datetime DateSold to format dd / mm / yy
                //Need Convert.ToDateTime because datatype of item.Datesold is DateTime?, not DateTime
                //Convert.ToDateTime(item.DateSold).ToString("dd/MM/yyyy")
                //if using Html.DisplayFor(), which makes it difficult to format the datetime string,...
                //must add attribute [DataType(DataType.Date)] in Model
                DateSold = Convert.ToDateTime(item.DateSold).ToString("dd/MM/yyyy"), // send date string dd/MM/yyyy
                CustomerName = item.Customer.Name,
                ProductName = item.Product.Name,
                StoreName = item.Store.Name
            });
            // datatype: List<{ Id, DateSold, CustomerName, ProductName, StoreName}>
            return Json(SalesToList.ToList(), JsonRequestBehavior.AllowGet);
        }
        
        // GET: Sales/Create
        public ActionResult Create()
        {
            var CustomerList = db.Customers.Select(item => new { item.Id, item.Name, item.Address });
            var ProductList = db.Products.Select(item => new { item.Id, item.Name, item.Price });
            var StoreList = db.Stores.Select(item => new { item.Id, item.Name, item.Address });

            return Json(
                new
                {
                    Id = 0,
                    CustomerId = CustomerList.First().Id,
                    ProductId = ProductList.First().Id,
                    StoreId = StoreList.First().Id,
                    DateSold = DateTime.Now.ToString("yyyy-MM-dd"),
                    CustomerList = db.Customers.Select(item => new { item.Id, item.Name, item.Address }),
                    ProductList = db.Products.Select(item => new { item.Id, item.Name, item.Price }),
                    StoreList = db.Stores.Select(item => new { item.Id, item.Name, item.Address })
                },JsonRequestBehavior.AllowGet);
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.ProductSolds.Add(productSold);
                db.SaveChanges();
                return Json(
                    new
                    {
                        // Because the data is saved successfully, the Modal will be closed, thus no need to return inputed data.
                        // Id < 0, which means data saved successfully
                        Id = -1
                    }, JsonRequestBehavior.AllowGet
                );
            }
            else
            {
                return Json(
                    new
                    {
                        Id = productSold.Id,
                        CustomerId = productSold.CustomerId,
                        ProductId = productSold.ProductId,
                        StoreId = productSold.StoreId,
                        DateSold = productSold.DateSold,
                        CustomerList = db.Customers.Select(item => new { item.Id, item.Name, item.Address }),
                        ProductList = db.Products.Select(item => new { item.Id, item.Name, item.Price }),
                        StoreList = db.Stores.Select(item => new { item.Id, item.Name, item.Address })
                    }, JsonRequestBehavior.AllowGet
                );
            }
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) // /Sales/edit/ 
            {   // HTTP Error 400.0 - Bad Request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSolds.Find(id);
            if (productSold == null) ///Sales/edit/wrongID
            {   // HTTP Error 404.0 - Not Found
                return HttpNotFound();
            }
            
            return Json(
                new
                {
                    Id = productSold.Id,
                    CustomerId = productSold.CustomerId,
                    ProductId = productSold.ProductId,
                    StoreId = productSold.StoreId,
                    DateSold = Convert.ToDateTime(productSold.DateSold).ToString("yyyy-MM-dd"),
                    CustomerList = db.Customers.Select(item => new { item.Id, item.Name, item.Address }),
                    ProductList = db.Products.Select(item => new { item.Id, item.Name, item.Price }),
                    StoreList = db.Stores.Select(item => new { item.Id, item.Name, item.Address })
                },JsonRequestBehavior.AllowGet
            );
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSold).State = EntityState.Modified;
                db.SaveChanges();
                return Json(
                    new
                    {
                        Id=-1
                    }, JsonRequestBehavior.AllowGet
                );
            }
            else    // ModelState.IsValid is false
            {   // did not save the data to database
                return Json(
                    new
                    {
                        Id = productSold.Id,
                        CustomerId = productSold.CustomerId,
                        ProductId = productSold.ProductId,
                        StoreId = productSold.StoreId,
                        DateSold = productSold.DateSold,
                        CustomerList = db.Customers.Select(item => new { item.Id, item.Name, item.Address }),
                        ProductList = db.Products.Select(item => new { item.Id, item.Name, item.Price }),
                        StoreList = db.Stores.Select(item => new { item.Id, item.Name, item.Address })
                    }, JsonRequestBehavior.AllowGet
                );
            }
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSolds.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            else
            {
                // return ProductSold Id, SoldDate, Customer name, Product Name, Store Name
                var returnProductSold = new
                {
                    Id = productSold.Id,
                    DateSold = Convert.ToDateTime(productSold.DateSold).ToString("dd/MM/yyyy"),
                    CustomerName = productSold.Customer.Name,
                    ProductName = productSold.Product.Name,
                    StoreName = productSold.Store.Name
                };
                return Json(returnProductSold, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductSold productSold = db.ProductSolds.Find(id);
            db.ProductSolds.Remove(productSold);
            db.SaveChanges();
            return Json(
                    new
                    {
                        // Because the data is saved successfully, the Modal will be closed, thus no need to return inputed data.
                        // Id < 0, which means data saved successfully
                        Id = -1
                    }, JsonRequestBehavior.AllowGet
                );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // Jump to test pages:
        //public ActionResult KODefaultOptionTest()
        //{
        //    return View();
        //}
        //public ActionResult KOValidationTest()
        //{
        //    return View();
        //}
    }
}
