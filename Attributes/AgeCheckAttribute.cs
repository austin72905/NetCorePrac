using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Attributes
{
    //要使用自定義的Valid Attribute ，要繼承ValidationAttribute
    public class AgeCheckAttribute : ValidationAttribute
    {
        public int MinAge { get; private set; }
        public int MaxAge { get; private set; }

        //建構子
        public AgeCheckAttribute(int minAge,int maxAge)
        {
            MinAge = minAge;
            MaxAge = maxAge;
        }
        //複寫 父類 IsVaild 方法
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //把輸入的值完成日期
            //ex 2010/1/1
            var date = Convert.ToDateTime(value);
            //驗證日期
            //ex2010+(MinAge:18) 至少要比今天大才會是18歲以上
            if (date.AddYears(MinAge)>DateTime.Today || date.AddYears(MaxAge)<DateTime.Today)
            {
                return new ValidationResult(ErrMsg(validationContext));
            }

            //如果正確
            return ValidationResult.Success;
        }
        //自定義的錯誤訊息
        private string ErrMsg(ValidationContext validationContext)
        {
            //有帶ErrorMessage 優先使用
            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                return this.ErrorMessage;
            }

            //如果沒有ErrorMessage，使用自定義的錯誤訊息
            return $"{validationContext.DisplayName} 時間不對";
        }
    }
}
