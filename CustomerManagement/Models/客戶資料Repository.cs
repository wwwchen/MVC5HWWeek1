using System;
using System.Linq;
using System.Collections.Generic;

namespace CustomerManagement.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public IQueryable<客戶資料> All(bool includeDelete)
        {
            if (includeDelete)
                return base.All();

            return this.All();
        }

        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(x => x.是否已刪除 == false);
        }

        public IQueryable<客戶資料> GetCustomerInfos(string customerName = null)
        {
            var data = this.All();

            if (!string.IsNullOrEmpty(customerName))
                data = data.Where(x => x.客戶名稱.Contains(customerName));

            return data;
        }

        public 客戶資料 GetSingleRecordByCustomerId(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = true;
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}