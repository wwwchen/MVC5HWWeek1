using System;
using System.Linq;
using System.Collections.Generic;
	
namespace CustomerManagement.Models
{   
	public  class V客戶統計資料Repository : EFRepository<V客戶統計資料>, IV客戶統計資料Repository
	{

	}

	public  interface IV客戶統計資料Repository : IRepository<V客戶統計資料>
	{

	}
}