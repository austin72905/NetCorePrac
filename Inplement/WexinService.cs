using NetCorePrac.Enum;
using NetCorePrac.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Inplement
{
    public class WexinService : IWalletService
    {
        //實作IWalletService
        public WalletType WalletType { get; } = WalletType.Wexin;
        public void Debit(decimal amount)
        {

        }
    }
}
