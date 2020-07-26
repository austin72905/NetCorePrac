using NetCorePrac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Interface
{
    public interface ISessionWapper
    {
        UserModel User { get; set; }
    }
}
