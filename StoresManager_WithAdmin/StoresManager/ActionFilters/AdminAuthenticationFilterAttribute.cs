using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoresManager.Entities;
using StoresManager.ExtentionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.ActionFilters
{
    public class AdminAuthenticationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetObject<User>("loggedUser").Username != "nikiv")
                context.Result = new RedirectResult("/Home/Index");
        }
    }
}
