using Microsoft.AspNetCore.Mvc;
using NetCorePrac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Controllers
{
    //當使用[Route("[controller]")]，MapRoute 會不再對ctrler or Action  作用
    //default 會失效，原本localhost:xxx/ 就能跳轉 ， 現在要localhost:xxx/test
    //controller 設定[Route]，Action 也要加[Route]，不然會抱錯
    //也可以指設特定Action
    //[Route("[controller]")]
    //[ApiController]  
    
    public class TestController : Controller
    {
        //[Route("")]
        public IActionResult Index()
        {
            return Content("hello world");
            //return Redirect("https://www.google.com.tw/maps/@23.546162,120.6402133,8z?hl=zh-TW");
        }
        [Route("lee")]//如果只有特定Action 加[Route] ，路徑會從網站根路徑開始算
        public IActionResult About()
        {
            return Redirect("https://www.mo-mo.com.tw/");
        }
        [Route("leesin")]
        public IActionResult Feature()
        {
            return Redirect("https://www.mo-mo.com.tw/feature");
        }
        [Route("model-bind")]
        //預設從Request 取值順序(Model Binding) Form  >  Route  >  QueryString
        //也可使用其他Binding Attribute  [FromForm]等 ，[FromBody]通常取Json
        //Xml 要在CnfgSrvc 加入AddXmlSerialFormatter
        public IActionResult ModelBind(int id)
        {
            return Content($"id : {id}");
        }
        [Route("ModelBindBag/{id?}")]//? {id?} 定義 id 為選擇性
        //測試Binding Attribute
        //[FromHeader]string header  : 他會從Header 尋找有沒有header這個參數
        public IActionResult ModelBindBag([FromHeader]string header,[FromForm]string form,[FromRoute]string id,[FromQuery]string query)
        {
            return Content($"header : {header},form : {form}, id : {id},query : {query}");
        }

        //[FromBody] 測試
        //這個沒辦法用，content tpye 加了也是一樣
        //用json格式傳入 ，測試 req 綁訂到自定義的UserModel
        [Route("aaa")]
        public IActionResult ModelBindJson([FromBody] UserModel userModel)
        {
                    
            return Ok(userModel);
        }
        //Model 驗證
        //詳見UserModel
        [Route("ModelBindForm")]
        public IActionResult ModelBindForm([FromForm] UserModel userModel)
        {
            if(userModel.Id<1)
            {
                ModelState.AddModelError("id", "無效商戶號");
                
            }


            if(ModelState.IsValid)
            {
                return Ok(userModel);
            }
            return BadRequest(ModelState);
        }
    }
}
