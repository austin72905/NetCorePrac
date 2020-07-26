using Microsoft.AspNetCore.Mvc;
using NetCorePrac.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Controllers
{
    public class SessionPracController : Controller
    {
        private readonly ISessionWapper _sessionWapper;
        //建構子:注入服務
        public SessionPracController(ISessionWapper sessionWapper)
        {
            _sessionWapper = sessionWapper;
        }

        //練習使用強行別接收Session
        public IActionResult Index()
        {
            var user = _sessionWapper.User;
            _sessionWapper.User = user;
            return Ok(user);
        }
    }
}
