using Microsoft.AspNetCore.Mvc;
using NetCorePrac.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.Controllers
{
    
    public class SampleController : Controller
    {
        private readonly ISample _sample;

        //建構子
        public SampleController(ISample sample) 
        {
            _sample = sample;
        }
        public string Index() 
        {
            return $"[{nameof(ISample)}] \r\n"
                  +$"Id: {_sample.Id} \r\n"
                  +$"type: {_sample.GetType()}";
        }
    }
}
