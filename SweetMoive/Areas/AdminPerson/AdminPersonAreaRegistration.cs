using System.Web.Mvc;

namespace SweetMoive.Areas.AdminPerson
{
    public class AdminPersonAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminPerson";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminPerson_default",
                "AdminPerson/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "SweetMoive.Areas.AdminPerson.Controllers" }
            );
        }
    }
}