using NetCorePrac.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Inplement
{
    //實作
    public class Sample: ISample
    {
        private static int _counter;
        //建構子
        public Sample()
        {
            Id = ++_counter;
        }
        public int Id { get; }
    }
}
