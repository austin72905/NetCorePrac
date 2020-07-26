using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace NetCorePrac.Extension
{
    //擴充方法必須在非泛型、靜態類別中定義
    public static class SessionExtsn
    {
        //自訂義一個泛型方法
        //https://ithelp.ithome.com.tw/articles/10099432
        public static void SetObj<T>(this ISession session,string key, T value)
        {
            session.SetString(key,JsonConvert.SerializeObject(value));
        }
        //取得物件
        public static T GetObj<T>(this ISession session,string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
