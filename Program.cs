using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetCorePrac
{
    public class Program
    {
        //程式進入點
        public static void Main(string[] args)
        {
            //在host產生之前設定前置準備
            //Build : 前置準備完成會建立WebHost    Run : 啟動 WebHost
            CreateHostBuilder(args).Build().Run();
        }


        //產生host的CreateHostBuilder方法
        //預設使用Generic Host(泛型主機)，實現IHostBuilder介面
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                              
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //產生WebHost要執行的類別
                    webBuilder.UseStartup<Startup>();
                });

        
    }
}
