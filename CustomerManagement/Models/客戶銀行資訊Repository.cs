using System;
using System.Linq;
using System.Collections.Generic;

namespace CustomerManagement.Models
{
    public class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(x => x.是否已刪除 == false);
        }

        public IQueryable<客戶銀行資訊> GetBankAccounts(int? customerId)
        {

            var data = this.All();

            if (customerId.HasValue && customerId.Value != 0)
                data = data.Where(x => x.客戶Id == customerId);

            return data;
        }

        public 客戶銀行資訊 GetSingleRecordByBankAccountId(int? id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }
    }

    public interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
    {

    }
}