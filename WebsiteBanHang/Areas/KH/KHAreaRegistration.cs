using System.Web.Mvc;

namespace WebsiteBanHang.Areas.KH
{
    public class KHAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KH";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KH_default",
                "KH/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}