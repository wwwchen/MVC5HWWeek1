using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerManagement.Models;

namespace CustomerManagement.Controllers
{
    public class CustomerContactController : Controller
    {
        private readonly 客戶聯絡人Repository _repoCustomerContact = RepositoryHelper.Get客戶聯絡人Repository();
        private readonly 客戶資料Repository _repoCustomer = RepositoryHelper.Get客戶資料Repository();

        private void SetViewBagCustomers(bool includeDelete = true)
        {
            ViewBag.Customers = new SelectList(_repoCustomer.All(includeDelete), "Id", "客戶名稱");
        }

        // GET: CustomerContact
        public ActionResult Index(string customerContactName, int? customerId, string jobTitle)
        {
            ViewData.Model = _repoCustomerContact.GetCustomerContacts(customerContactName: customerContactName,
                customerId: customerId, jobTitle: jobTitle);

            SetViewBagCustomers();

            return View();
        }

        // GET: CustomerContact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = _repoCustomerContact.GetSingleRecordByCustomerContactId(id.Value);

            if (data == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = data;

            return View();
        }

        // GET: CustomerContact/Create
        public ActionResult Create()
        {
            SetViewBagCustomers(includeDelete: false);

            return View();
        }

        // POST: CustomerContact/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 customerContact)
        {
            if (ModelState.IsValid)
            {
                _repoCustomerContact.Add(customerContact);
                _repoCustomerContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            SetViewBagCustomers(includeDelete: false);

            ViewData.Model = customerContact;

            return View();
        }

        // GET: CustomerContact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = _repoCustomerContact.GetSingleRecordByCustomerContactId(id.Value);

            if (data == null)
            {
                return HttpNotFound();
            }

            SetViewBagCustomers();

            ViewData.Model = data;

            return View();
        }

        // POST: CustomerContact/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 customerContact, FormCollection form)
        {
            ;
            var data = _repoCustomerContact.GetSingleRecordByCustomerContactId(customerContact.Id);

            if (TryUpdateModel(data, ""
                , form.AllKeys, new string[] { "Id" }))
            {
                _repoCustomerContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            SetViewBagCustomers();

            ViewData.Model = data;

            return View();
        }

        // GET: CustomerContact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = _repoCustomerContact.GetSingleRecordByCustomerContactId(id.Value);

            if (data == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = data;

            return View();
        }

        // POST: CustomerContact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var data = _repoCustomerContact.GetSingleRecordByCustomerContactId(id);

            _repoCustomerContact.Delete(data);
            _repoCustomerContact.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }
    }
}
