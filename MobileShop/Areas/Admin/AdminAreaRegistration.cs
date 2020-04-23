﻿using System.Web.Mvc;

namespace MobileShop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", controller = "Cate", id = UrlParameter.Optional },
                new[] { "MobileShop.Areas.Admin.Controllers" }
            );
        }
    }
}