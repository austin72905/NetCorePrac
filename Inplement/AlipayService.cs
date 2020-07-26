using NetCorePrac.Enum;
using NetCorePrac.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Inplement
{
    //實作IWalletService
    public class AlipayService :IWalletService
    {
        public WalletType WalletType { get; } = WalletType.Alipay;
        public void Debit(decimal amount)
        {

        }
    }
}
