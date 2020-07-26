using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePrac.MidWrePrac
{
    public static class CustomMdwreExtnsion
    {
        public static IApplicationBuilder UseFirCusMdwre(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FtMidwre>();
        }
    }
}
