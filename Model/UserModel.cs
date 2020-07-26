using Microsoft.AspNetCore.SignalR;
using NetCorePrac.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Model
{
    public class UserModel
    {
        //可加上Valid Attribute 驗證
        //https://docs.microsoft.com/zh-tw/dotnet/api/system.componentmodel.dataannotations?view=netcore-2.0&fbclid=IwAR2BwYphwqCtqSLlZRH96abmdKkextW_LL4iDYzsJcaRCEjy1o_LX3_uTp0
        //也可自訂義Valid Attribute
        [Required]
        public int Id { get; set; }
        
        [StringLength(5,MinimumLength =4)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [StringLength(200)]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        //使用自定義的ValidationAttribute
        //限制最小18歲，最大100歲
        [AgeCheck(18,100)]
        public DateTime BirthDate { get; set; }

    }
}
