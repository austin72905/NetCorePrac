using Microsoft.AspNetCore.Http;
using NetCorePrac.Interface;
using NetCorePrac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Extension
{
    //實作ISessionWapper
    public class SessionWapper : ISessionWapper
    {
        private static readonly string _userKey = "session.User";
        private readonly IHttpContextAccessor _httpContextAccessor;
        //建構子
        public SessionWapper(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }
        //可以抓到之前寫的Session擴充方法
        private ISession Session
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session;
            }
        }
        //實作
        public UserModel User
        {
            get
            {
                return Session.GetObj<UserModel>(_userKey);
            }
            set
            {
                Session.SetObj(_userKey, value);
            }
        }
    }
}
