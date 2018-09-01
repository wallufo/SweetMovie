using System.Web.Mvc;

namespace SweetMoive.Areas.MovieUser
{
    public class MovieUserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MovieUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MovieUser_default",
                "MovieUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "SweetMoive.Areas.MovieUser.Controllers" }
            );
        }
    }
}