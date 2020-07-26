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
        //建構子
        public Startup(IConfiguration configuration)
        {
            //下面的那行IConfiguration
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //Host建置時呼叫
        //可不實作
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //在此註冊服務到DI容器
            //實體的產生，不會再使用方new 而是註冊在DI容器(services)
            //運作方式: 把實體化的物件從建構子傳入
            //分3類  生命週期
            //Scoped  :  每個Request 都拿同一個實例
            //Transient : 只要跟DI容器請求這個類型，就會取得新的實例
            //Singleton : 一旦題例化就會一直存在容器中

            services.AddScoped<ISample, Sample>();//<注入的類別,實作的類別>
            //自己建立Service Provider
            //Host builder後會自動設置Service Provider，與自己建立的是完全不同的實體，不同的實體會各自存在自己的DI容器中
            //注入使用相同interface 的service
            services.AddSingleton<IWalletService, AlipayService>();
            services.AddSingleton<IWalletService, WexinService>();
            services.AddSingleton<IWalletService, FastpayService>();
            //通常只建議Singleton服務這樣用，其他兩個注入就會產生新實例，沒用到會造成效能耗損

            //註冊MVC 路由
            //要加裡面的EnableEndpointRouting=false 不然抱錯
            services.AddMvc(options => options.EnableEndpointRouting = false);

            //加入Session 服務
            //將session 存在 .net core的記憶體中
            services.AddDistributedMemoryCache();
            //安全性: 可在裡面設定
            services.AddSession(options=> 
            {
                //現在只能用https連線
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = "AustinPCSession";//自己設定 Session 名稱
                options.IdleTimeout = TimeSpan.FromMinutes(5);//設定session 過期時間
            });

            //註冊IHttpContextAccessor、ISessionWapper
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ISessionWapper, SessionWapper>();






        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        //.Host啟動時呼叫
        //一定要實作
        //IApplicationBuilder必要參數，request 進出的Pipeline都是由這個設定
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //MiddlerWare 在此註冊  ，使用IApplicationBuilder 裡 Use 
            //MiddlerWare 出請求(Request)之後，到接收回應(Response)這段來回的途徑上(pipeline)，用來處理特定用途的程式。ex: 鑰匙掉了，你會在今天路過的地方找
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //這些都是Use擴充方法
            //Use 註冊順序很重要(先進後出，有點像S行排序)， 可用arg.InVoke他就會先執行下一個Midwre
            app.UseRouting(); //Request 路由使用的MiddleWare

            app.UseAuthorization();

            //使用靜態檔案: 沒辦法放在根目錄查看， 
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "StaticFiles")),//資料夾名//StaticFiles
                RequestPath = new PathString("/StaticFiles")//路由名
            });//預設啟用根目錄是wwwroot， Request 檔案不存在 則會轉向Run事件

            //ctrl+k,ctrl+c 註解 ctrl+k,ctrl+u 取消
            //https://blog.darkthread.net/blog/aspnetcore-middleware-lab/
            //UseEndpoints 之後就不會再處理後面的MidWre
            //app.UseEndpoints(endpoints =>
            //{
            //    //預設
            //    //endpoints.MapControllers();
            //    // MapControllerRoute 是用來建立單一路由。 單一路由的名稱為 default route REST Api 應該使用屬性路由。
            //    //? {id?} 定義 id 為選擇性
            //    //https://docs.microsoft.com/zh-tw/aspnet/core/mvc/controllers/routing?view=aspnetcore-3.1
            //    endpoints.MapControllerRoute("default", "{controller=Sample}/{action=Index}/{id?}");

            //});

            //app.run() 是最後一個行為，能使用完整Req、Res，但不能串Midwre
            ////app.map()可以註冊新路由
            //app.Use(async (c, n) =>
            //{
            //    await c.Response.WriteAsync("first midwre \r\n");
            //    //用Invoke 串聯Midwre
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
            ////全域註冊Midwre方法
            ////app.UseMiddleware<SecMidwre>();
            //app.UseMiddleware<FtMidwre>();
            ////區域註冊方法
            ////在特定Controller [MiddlewareFilter(typeof(SecMidwre))] 裡面的街口也加 [MiddlewareFilter(typeof(SecMidwre))]
            ////Extension Midwre 方法(自製擴充方法)
            //app.UseFirCusMdwre();
            //app.UseMiddleware<GetReqMdwre>();
            //app.Run(async c =>
            //{
            //    await c.Response.WriteAsync("hello world \r\n");
            //});

            //這個放在UseMvc 後面就不作用了
            ////設定Cookeis 每個Request Client都會把Cookies 送到 Server
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

            //設定Session
            //Session : 透過"Cookie"內的唯一識別，將用戶資料存載Server，每次Request 都會帶上這個值，當Session服務取得這個值，就會去Session容器找出這個值得Session 資料
            app.UseSession();
            //app.Run(async (context) =>
            //{
            //    string msg;
            //    //設定Session 資料
            //    context.Session.SetString("Sample", "this is session");
            //    //從Session 值內拿到資料
            //    msg = context.Session.GetString("Sample");
            //    await context.Response.WriteAsync($"{msg }");
            //    //run 完會看到cookies 多了一個 Asp.net core.Session
            //    //新版的Session 如果要放物件資料，要自己序列化
            //    //詳見SessionExtsn
            //});



            //設定MVC路由

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "about",
                    template: "about",//URL 打 localhost:xxx/test/about 根 localhost:xxx/about  一樣
                    defaults: new { controller = "Test", action = "About" }
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",//輸入的URL 格式
                    defaults: new { controller = "Test", action = "Index" }//預設跑這個街口
                    );
                routes.MapRoute(
                   name: "feature",
                   template: "{controller}/{action}/{id?}",//輸入的URL 格式
                   defaults: new { controller = "Test", action = "feature" }
                   );

                
            });

            

        }
    }
}
