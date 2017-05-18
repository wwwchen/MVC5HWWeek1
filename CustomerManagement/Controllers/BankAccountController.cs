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
    public class BankAccountController : Controller
    {
        private readonly 客戶銀行資訊Repository _repoBankAccount = RepositoryHelper.Get客戶銀行資訊Repository();
        private readonly 客戶資料Repository _repoCustomer = RepositoryHelper.Get客戶資料Repository();

        private void SetViewBagCustomers(bool includeDelete = true)
        {
            ViewBag.Customers = new SelectList(_repoCustomer.All(includeDelete), "Id", "客戶名稱");
        }

        // GET: BankAccount
        public ActionResult Index(int? customerId)
        {
            ViewData.Model = _repoBankAccount.GetBankAccounts(customerId: customerId);

            SetViewBagCustomers();

            return View();
        }

        // GET: BankAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = _repoBankAccount.GetSingleRecordByBankAccountId(id);


            if (data == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = data;

            return View();
        }

        // GET: BankAccount/Create
        public ActionResult Create()
        {
            SetViewBagCustomers(includeDelete: false);
            return View();
        }

        // POST: BankAccount/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 bankAccount)
        {
            if (ModelState.IsValid)
            {
                _repoBankAccount.Add(bankAccount);
                _repoBankAccount.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            SetViewBagCustomers(includeDelete: false);

            ViewData.Model = bankAccount;

            return View();
        }

        // GET: BankAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = _repoBankAccount.GetSingleRecordByBankAccountId(id.Value);

            if (data == null)
            {
                return HttpNotFound();
            }

            SetViewBagCustomers();

            ViewData.Model = data;

            return View();
        }

        // POST: BankAccount/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 bankAccount, FormCollection form)
        {
            var data = _repoBankAccount.GetSingleRecordByBankAccountId(bankAccount.Id);

            if (TryUpdateModel(data, ""
                , form.AllKeys, new string[] { "Id" }))
            {
                _repoBankAccount.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            SetViewBagCustomers(); ;

            ViewData.Model = data;

            return View();
        }

        // GET: BankAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = _repoBankAccount.GetSingleRecordByBankAccountId(id);

            if (data == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = data;

            return View();
        }

        // POST: BankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var data = _repoBankAccount.GetSingleRecordByBankAccountId(id);

            _repoBankAccount.Delete(data);
            _repoBankAccount.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }
    }
}
