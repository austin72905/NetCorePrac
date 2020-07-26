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
        //�{���i�J�I
        public static void Main(string[] args)
        {
            //�bhost���ͤ��e�]�w�e�m�ǳ�
            //Build : �e�m�ǳƧ����|�إ�WebHost    Run : �Ұ� WebHost
            CreateHostBuilder(args).Build().Run();
        }


        //����host��CreateHostBuilder��k
        //�w�]�ϥ�Generic Host(�x���D��)�A��{IHostBuilder����
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                              
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //����WebHost�n���檺���O
                    webBuilder.UseStartup<Startup>();
                });

        
    }
}
