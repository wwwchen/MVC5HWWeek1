using System;
using System.Linq;
using System.Collections.Generic;

namespace CustomerManagement.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(x => x.是否已刪除 == false);
        }

        public 客戶聯絡人 GetSingleRecordByCustomerContactId(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }


        public IQueryable<客戶聯絡人> GetCustomerContacts(string customerContactName, int? customerId)
        {
            var data = this.All();

            if (!string.IsNullOrEmpty(customerContactName))
                data = data.Where(x => x.姓名.Contains(customerContactName));

            if (customerId.HasValue && customerId.Value != 0)
                data = data.Where(x => x.客戶Id == customerId);

            return data;
        }
    }

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}