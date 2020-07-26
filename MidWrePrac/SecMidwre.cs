using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.MidWrePrac
{
    public class SecMidwre
    {
        private readonly RequestDelegate _nextInvoke;
        public SecMidwre(RequestDelegate nextInvoke)
        {
            _nextInvoke = nextInvoke;
        }
        public async Task Invoke(HttpContext c)
        {
            await c.Response.WriteAsync("second midwre \r\n");
            //調用下一個Middleware
            await _nextInvoke(c);
            await c.Response.WriteAsync("second out \r\n");
        }
    }
}
