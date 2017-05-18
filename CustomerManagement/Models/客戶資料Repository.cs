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

        public IQueryable<客戶資料> GetCustomerInfos(string customerName, string category)
        {
            var data = this.All();

            if (!string.IsNullOrEmpty(customerName))
                data = data.Where(x => x.客戶名稱.Contains(customerName));

            if (!string.IsNullOrEmpty(category))
                data = data.Where(x => x.客戶分類 == category);

            return data;
        }

        public Dictionary<string, string> GetCategorys()
        {
            Dictionary<string, string> dictionary = base.All().Select(x => new { key = x.客戶分類, value = x.客戶分類 })
                .Distinct().ToDictionary(x => x.key, y => y.value);

            return dictionary;
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