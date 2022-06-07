using System.Web.Mvc;

namespace WebsiteBanHang.Areas.NV
{
    public class NVAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NV";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NV_default",
                "NV/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}