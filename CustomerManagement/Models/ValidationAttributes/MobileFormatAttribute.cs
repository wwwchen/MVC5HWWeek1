using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CustomerManagement.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MobileFormatAttribute : DataTypeAttribute
    {
        public MobileFormatAttribute() : base(DataType.Text)
        {
            ErrorMessage = "電話格式錯誤，必需為09xx-xxxxxx";
        }

        public override bool IsValid(object value)
        {
            var str = (string)value;
            if (string.IsNullOrWhiteSpace(str))
                return true;

            Regex rgx = new Regex("\\d{4}-\\d{6}");
            var checkResults = rgx.Matches(str);

            if (checkResults.Count > 0)
                return true;

            return false;
        }
    }
}