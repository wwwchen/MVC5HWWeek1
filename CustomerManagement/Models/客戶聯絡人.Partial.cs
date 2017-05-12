using System.Linq;
using System.Web.Mvc;
using CustomerManagement.Models.ValidationAttributes;

namespace CustomerManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            客戶資料Entities db = new 客戶資料Entities();
            var 客戶聯絡人 = db.客戶聯絡人.AsQueryable();
            bool checkExistingEmail = 客戶聯絡人.Any(x => x.是否已刪除 == false && x.客戶Id == 客戶Id && x.Email == Email);

            if (checkExistingEmail)
            {
                yield return new ValidationResult("已經存在相同的 Email", new string[] { "Email" }); //一個label只能有一個錯誤訊息
            }

            yield break;
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [MobileFormat]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
