using System;
using System.Linq;
using System.Collections.Generic;

namespace CustomerManagement.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(x => x.是否已刪除 == false);
        }

        public IQueryable<客戶資料> GetCustomerInfoList(string customerName = null)
        {
            var data = this.All();

            if (!string.IsNullOrEmpty(customerName))
                data.Where(x => x.客戶名稱.Contains(customerName));

            return data;
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}