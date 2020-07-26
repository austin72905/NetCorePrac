using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using NetCorePrac.Extension;
using NetCorePrac.Inplement;
using NetCorePrac.Interface;
using NetCorePrac.Model;
using System;
using System.IO;

namespace NetCorePrac
{
    public class Startup
    {
        //�غc�l
        public Startup(IConfiguration configuration)
        {
            //�U��������IConfiguration
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //Host�ظm�ɩI�s
        //�i����@
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //�b�����U�A�Ȩ�DI�e��
            //���骺���͡A���|�A�ϥΤ�new �ӬO���U�bDI�e��(services)
            //�B�@�覡: �����ƪ�����q�غc�l�ǤJ
            //��3��  �ͩR�g��
            //Scoped  :  �C��Request �����P�@�ӹ��
            //Transient : �u�n��DI�e���ШD�o�������A�N�|���o�s�����
            //Singleton : �@���D�ҤƴN�|�@���s�b�e����

            services.AddScoped<ISample, Sample>();//<�`�J�����O,��@�����O>
            //�ۤv�إ�Service Provider
            //Host builder��|�۰ʳ]�mService Provider�A�P�ۤv�إߪ��O�������P������A���P������|�U�ۦs�b�ۤv��DI�e����
            //�`�J�ϥάۦPinterface ��service
            services.AddSingleton<IWalletService, AlipayService>();
            services.AddSingleton<IWalletService, WexinService>();
            services.AddSingleton<IWalletService, FastpayService>();
            //�q�`�u��ĳSingleton�A�ȳo�˥ΡA��L��Ӫ`�J�N�|���ͷs��ҡA�S�Ψ�|�y���į�ӷl

            //���UMVC ����
            //�n�[�̭���EnableEndpointRouting=false ���M���
            services.AddMvc(options => options.EnableEndpointRouting = false);

            //�[�JSession �A��
            //�Nsession �s�b .net core���O���餤
            services.AddDistributedMemoryCache();
            //�w����: �i�b�̭��]�w
            services.AddSession(options=> 
            {
                //�{�b�u���https�s�u
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = "AustinPCSession";//�ۤv�]�w Session �W��
                options.IdleTimeout = TimeSpan.FromMinutes(5);//�]�wsession �L���ɶ�
            });

            //���UIHttpContextAccessor�BISessionWapper
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ISessionWapper, SessionWapper>();






        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        //.Host�ҰʮɩI�s
        //�@�w�n��@
        //IApplicationBuilder���n�ѼơArequest �i�X��Pipeline���O�ѳo�ӳ]�w
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //MiddlerWare �b�����U  �A�ϥ�IApplicationBuilder �� Use 
            //MiddlerWare �X�ШD(Request)����A�챵���^��(Response)�o�q�Ӧ^���~�|�W(pipeline)�A�ΨӳB�z�S�w�γ~���{���Cex: �_�ͱ��F�A�A�|�b���Ѹ��L���a���
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //�o�ǳ��OUse�X�R��k
            //Use ���U���ǫܭ��n(���i��X�A���I��S��Ƨ�)�A �i��arg.InVoke�L�N�|������U�@��Midwre
            app.UseRouting(); //Request ���ѨϥΪ�MiddleWare

            app.UseAuthorization();

            //�ϥ��R�A�ɮ�: �S��k��b�ڥؿ��d�ݡA 
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "StaticFiles")),//��Ƨ��W//StaticFiles
                RequestPath = new PathString("/StaticFiles")//���ѦW
            });//�w�]�ҥήڥؿ��Owwwroot�A Request �ɮפ��s�b �h�|��VRun�ƥ�

            //ctrl+k,ctrl+c ���� ctrl+k,ctrl+u ����
            //https://blog.darkthread.net/blog/aspnetcore-middleware-lab/
            //UseEndpoints ����N���|�A�B�z�᭱��MidWre
            //app.UseEndpoints(endpoints =>
            //{
            //    //�w�]
            //    //endpoints.MapControllers();
            //    // MapControllerRoute �O�Ψӫإ߳�@���ѡC ��@���Ѫ��W�٬� default route REST Api ���Өϥ��ݩʸ��ѡC
            //    //? {id?} �w�q id ����ܩ�
            //    //https://docs.microsoft.com/zh-tw/aspnet/core/mvc/controllers/routing?view=aspnetcore-3.1
            //    endpoints.MapControllerRoute("default", "{controller=Sample}/{action=Index}/{id?}");

            //});

            //app.run() �O�̫�@�Ӧ欰�A��ϥΧ���Req�BRes�A�������Midwre
            ////app.map()�i�H���U�s����
            //app.Use(async (c, n) =>
            //{
            //    await c.Response.WriteAsync("first midwre \r\n");
            //    //��Invoke ���pMidwre
            //    await n.Invoke();
            //    await c.Response.WriteAsync("first out \r\n");
            //});

            ////localhost:59879/sec
            //app.Map("/sec", newRoute =>
            // {
            //     newRoute.Use(async (c, n) =>
            //     {
            //         await c.Response.WriteAsync("i m new route \r\n");
            //         await n.Invoke();
            //         await c.Response.WriteAsync("i m new route out \r\n");
            //     });
            //     newRoute.Run(async c =>
            //     {
            //         await c.Response.WriteAsync(" route done \r\n");
            //     });
            // });
            ////������UMidwre��k
            ////app.UseMiddleware<SecMidwre>();
            //app.UseMiddleware<FtMidwre>();
            ////�ϰ���U��k
            ////�b�S�wController [MiddlewareFilter(typeof(SecMidwre))] �̭�����f�]�[ [MiddlewareFilter(typeof(SecMidwre))]
            ////Extension Midwre ��k(�ۻs�X�R��k)
            //app.UseFirCusMdwre();
            //app.UseMiddleware<GetReqMdwre>();
            //app.Run(async c =>
            //{
            //    await c.Response.WriteAsync("hello world \r\n");
            //});

            //�o�ө�bUseMvc �᭱�N���@�ΤF
            ////�]�wCookeis �C��Request Client���|��Cookies �e�� Server
            //app.Run(async (context) =>
            //{
            //    string msg;
            //    if (!context.Request.Cookies.TryGetValue("AustinPC", out msg))
            //    {
            //        msg = "Save data to cookies";
            //    }
            //    context.Response.Cookies.Append("AustinPC", "this is your cookie");
            //    await context.Response.WriteAsync($"{msg}");
            //});

            //�]�wSession
            //Session : �z�L"Cookie"�����ߤ@�ѧO�A�N�Τ��Ʀs��Server�A�C��Request ���|�a�W�o�ӭȡA��Session�A�Ȩ��o�o�ӭȡA�N�|�hSession�e����X�o�ӭȱoSession ���
            app.UseSession();
            //app.Run(async (context) =>
            //{
            //    string msg;
            //    //�]�wSession ���
            //    context.Session.SetString("Sample", "this is session");
            //    //�qSession �Ȥ�������
            //    msg = context.Session.GetString("Sample");
            //    await context.Response.WriteAsync($"{msg }");
            //    //run ���|�ݨ�cookies �h�F�@�� Asp.net core.Session
            //    //�s����Session �p�G�n�񪫥��ơA�n�ۤv�ǦC��
            //    //�Ԩ�SessionExtsn
            //});



            //�]�wMVC����

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "about",
                    template: "about",//URL �� localhost:xxx/test/about �� localhost:xxx/about  �@��
                    defaults: new { controller = "Test", action = "About" }
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",//��J��URL �榡
                    defaults: new { controller = "Test", action = "Index" }//�w�]�]�o�ӵ�f
                    );
                routes.MapRoute(
                   name: "feature",
                   template: "{controller}/{action}/{id?}",//��J��URL �榡
                   defaults: new { controller = "Test", action = "feature" }
                   );

                
            });

            

        }
    }
}
