using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.MidWrePrac
{
    //將MidWre 獨立出來
    public class FtMidwre
    {
        //透過建構子所傳入的RequestDelegate，來參考到Pipeline裡的下一個Middleware。透過調用RequestDelegate，就可以調用Pipeline裡的下一個Middleware的Invoke方法
        private readonly RequestDelegate _next;
        //建構子
        public FtMidwre(RequestDelegate next)
        {
            _next = next;
        }

        //Invoke 是必須名稱
        public async Task Invoke(HttpContext c)
        {
            
            await c.Response.WriteAsync("third in \r\n");
            //可以調用到下一個Middleware
            await _next(c);
            await c.Response.WriteAsync("third out \r\n");
        }
    }
}
