using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MobileShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "Auth", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Logout",
                url: "dang-xuat",
                defaults: new { controller = "Auth", action = "Logout", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ki",
                defaults: new { controller = "Auth", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Forgot",
                url: "quen-mat-khau",
                defaults: new { controller = "Auth", action = "Forgot", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Change",
                url: "doi-mat-khau",
                defaults: new { controller = "Auth", action = "ChangePass", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Home",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Cate",
                url: "san-pham/{metatitle}-{id}",
                defaults: new { controller = "Cate", action = "Type", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Detail",
                url: "chi-tiet/{metatitle}-{id}",
                defaults: new { controller = "Detail", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );
            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Home", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "CartIndex",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "CartAdd",
                url: "mua-san-pham/{metatitle}-{id}",
                defaults: new { controller = "Cart", action = "AddCart", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Delcarts",
                url: "xoa-san-pham-{id}",
                defaults: new { controller = "Cart", action = "Delete", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MobileShop.Controllers" }
            );
        }
    }
}
