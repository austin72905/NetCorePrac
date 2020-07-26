using NetCorePrac.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Interface
{
    public interface IWalletService
    {
       //emum
        WalletType WalletType { get; }

        //定義一個Debit 方法
        void Debit(decimal amount);
    }
}
