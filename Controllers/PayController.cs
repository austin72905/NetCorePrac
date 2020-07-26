using Microsoft.AspNetCore.Mvc;
using NetCorePrac.Enum;
using NetCorePrac.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Controllers
{
    public class PayController : Controller
    {
        private readonly IEnumerable<IWalletService> _walletService;
        //建構子
        //如果注入不是使用IEnumerable<T>，舊址會得到最後註冊的FastpayService
        public PayController(IEnumerable<IWalletService> walletService)
        {
            _walletService = walletService;
        }

        public IActionResult PayCheck(WalletType walletType,decimal amount)
        {
            //Single 有，且僅有一個.
            var walletService = _walletService.Single(x => x.WalletType == walletType);
            return Ok();
        }
    }
}
