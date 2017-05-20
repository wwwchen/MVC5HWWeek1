using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CustomerManagement.Models;
using CustomerManagement.Models.ActionFilterAttributes;
using CustomerManagement.Models.FilterAttributes;
using X.PagedList;

namespace CustomerManagement.Controllers
{
    [ActionInterval]
    [ActionResultInterval]
    public class CustomerInfoController : Controller
    {
        private readonly 客戶資料Repository _repoCustomer = RepositoryHelper.Get客戶資料Repository();
        private readonly V客戶統計資料Repository _repoCustomerStatistic = RepositoryHelper.GetV客戶統計資料Repository();

        // GET: CustomerInfo
        [ActionInterval]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index(string customerName, string category)
        {

            ViewBag.Categorys = new SelectList(_repoCustomer.GetCategorys(), "key", "value"); ;

            return View();
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public async Task<PartialViewResult> PagedPartial(
            int? page, string customerName, string category)
        {
            var data = await _repoCustomer.GetCustomerInfos(customerName: customerName, category: category).ToListAsync();

            int pageIndex = page ?? 1;
            int pageSize = 3;
            int totalCount = 0;

            totalCount = data.Count();

            var s = data.OrderBy(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var pageResult = new StaticPagedList<CustomerManagement.Models.客戶資料>(s, pageIndex, pageSize, totalCount);

            return PartialView("_CustomerInfoPagedPartial", pageResult);
        }

        // GET: CustomerInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = _repoCustomer.GetSingleRecordByCustomerId(id.Value);

            if (data == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = data;

            return View();
        }

        // GET: CustomerInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerInfo/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DbUpdateError]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                _repoCustomer.Add(客戶資料);
                _repoCustomer.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: CustomerInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = _repoCustomer.GetSingleRecordByCustomerId(id.Value);

            if (data == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = data;

            return View();
        }

        // POST: CustomerInfo/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DbUpdateError]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 customerInfo, FormCollection form)
        {
            var data = _repoCustomer.GetSingleRecordByCustomerId(customerInfo.Id);

            if (TryUpdateModel(data
                , "", form.AllKeys, new string[] { "Id" }))
            {
                _repoCustomer.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(data);
        }

        // GET: CustomerInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = _repoCustomer.GetSingleRecordByCustomerId(id.Value);

            if (data == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = data;

            return View();
        }

        // POST: CustomerInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [DbUpdateError]
        public ActionResult DeleteConfirmed(int id)
        {
            var data = _repoCustomer.GetSingleRecordByCustomerId(id);

            _repoCustomer.Delete(data);
            _repoCustomer.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult CustomerStatistics()
        {
            ViewData.Model = _repoCustomerStatistic.All();
            return View();
        }
    }
}
