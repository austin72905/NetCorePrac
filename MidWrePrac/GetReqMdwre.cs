using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.MidWrePrac
{
    public class GetReqMdwre
    {
        private readonly RequestDelegate _next;
        public GetReqMdwre(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string reqStream;

            //讀取資料流
            using (var reader=new StreamReader(context.Request.Body))
            {
                reqStream = await reader.ReadToEndAsync();
                if(string.IsNullOrEmpty(reqStream))
                {
                    await _next(context);
                }
                else
                {
                    //把stream positoin 還原
                    //可是加了這一航後面的mdwre都沒東西
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                }
                
                
                
            }
            await _next(context);
            Console.WriteLine($"{reqStream}");
            await context.Response.WriteAsync("get req body out \r\n");

        }
    }
}
