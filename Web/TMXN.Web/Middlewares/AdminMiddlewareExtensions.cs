using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMXN.Web.Middlewares
{
    public static class AdminMiddlewareExtensions
    {
        public static IApplicationBuilder UseAdminMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AdminMiddleware>();
        }
    }
}
