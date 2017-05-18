using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.Models.FilterAttributes
{
    public class DbUpdateErrorAttribute : HandleErrorAttribute
    {
        public DbUpdateErrorAttribute()
        {
            ExceptionType = typeof(DbUpdateException);
            View = "Error_DbUpdateException";
        }
    }
}