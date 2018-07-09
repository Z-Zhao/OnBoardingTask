using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using OnBoardingTask2018Jun.Models;
using OnBoardingTask2018Jun.Models.New_Models;

namespace OnBoardingTask2018Jun.Controllers
{
    // enum ActionStates is in Model - New Model - ActionStates.cs
    public class CustomersController : Controller
    {
        private CustomChangedEntities db = new CustomChangedEntities();

        // GET: Customers
        public ActionResult Index()
        {
            CustomerControllerViewDataTransferModel ReturnToIndex = new CustomerControllerViewDataTransferModel();
            ReturnToIndex.DbCustomerSet = db.Customers.ToList();
            ReturnToIndex.ActionState = (int)ActionStates.IndexGet;
            ReturnToIndex.TransferExtraCustomer = null;

            return View("Index",ReturnToIndex);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            CustomerControllerViewDataTransferModel ReturnToIndex = new CustomerControllerViewDataTransferModel();
            ReturnToIndex.DbCustomerSet = db.Customers.ToList();
            ReturnToIndex.ActionState = (int)ActionStates.CreateGet;
            ReturnToIndex.TransferExtraCustomer = new Customer { Name = "", Address = "" };

            return View("Index", ReturnToIndex);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] // Avoid CSRF: Cross Site Request Forgery
        public ActionResult Create([Bind(Include = "Id,Name,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // return to view Index, data is combined with...
                // original data db.Customers, and item "customer"...
                // i.e., the new inputed one().
                // because the new inputed item is not valid, give this item a special ID = -1
                // customer.Id = -1;

                CustomerControllerViewDataTransferModel ReturnToIndex = new CustomerControllerViewDataTransferModel();
                ReturnToIndex.DbCustomerSet = db.Customers.ToList();
                ReturnToIndex.ActionState = (int)ActionStates.CreateInputInvalid;
                ReturnToIndex.TransferExtraCustomer = customer;
                return View("Index", ReturnToIndex);

                // After receive this ReturnToIndex, the Index.cshtml will proceess this variable,... 
                // according to .TransferExtraCustomer & .ActionState, copy ExtraCustomer to partial view:...
                // Create page, then list .DbCustomerSet items in the table on Index page
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                CustomerControllerViewDataTransferModel ReturnToIndex = new CustomerControllerViewDataTransferModel();
                ReturnToIndex.DbCustomerSet = db.Customers.ToList();
                ReturnToIndex.ActionState = (int)ActionStates.EditGet;
                ReturnToIndex.TransferExtraCustomer = customer;

                return View("Index", ReturnToIndex);

                // Compare different View methods:
                // (1)when using $("#EditModal").modal() to open modal, return to View("Index").
                // when using data-target=... data-toggle=.. it is not good, because the View(Index) will show up on modal webpage

                // (2) return View(customer); wrong, because it equals to View("Edit",customer)- "Edit" is not a correct url

                // (3) when using data-target=... data-toggle=.. it is good, because the correct View(Edit/id) will show up on modal webpage
                // but data transfer to the "Edit.cshtml", not the index page. Thus the textbox in modal is empty.
                // return View("Edit/"+ OldId, customer); // OK, but data not return to the Index page
            }
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                CustomerControllerViewDataTransferModel ReturnToIndex = new CustomerControllerViewDataTransferModel();
                ReturnToIndex.DbCustomerSet = db.Customers.ToList();
                ReturnToIndex.ActionState = (int)ActionStates.EditInputInvalid;
                ReturnToIndex.TransferExtraCustomer = customer;

                return View("Index", ReturnToIndex);
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    CustomerControllerViewDataTransferModel ReturnToIndex = new CustomerControllerViewDataTransferModel();
                    ReturnToIndex.DbCustomerSet = db.Customers.ToList();
                    ReturnToIndex.ActionState = (int)ActionStates.DeleteGet;
                    ReturnToIndex.TransferExtraCustomer = customer;

                    // different ways of View V.S. Openning Modal analysis: see comment in GET: Customers/Edit/5
                    return View("Index", ReturnToIndex);
                }
            }
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
