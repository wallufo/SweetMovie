using System.Web;
using System.Web.Optimization;

namespace SweetMoive
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/LoginJs").Include(
                    "~/Scripts/parallax.js",
                    "~/Scripts/stopExecutionOnTimeout.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/Layout").Include(
                    "~/Scripts/bootsnav.js",
                    "~/Scripts/bootstrap.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/star").Include(
                    "~/Scripts/jquery.star-rating-svg.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/Index").Include(
                    "~/Scripts/jquery.immersive-slider.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/moviedetail").Include(
                    "~/Scripts/bootstrap-dialog.js",
                    "~/Scripts/video.js",
                    "~/Scripts/lightgallery-all.min.js",
                    "~/Scripts/bubbly-bg.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/userinfo").Include(
                    "~/Scripts/coreNavigation.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/avatar").Include(
                    "~/Scripts/hammer.js",
                    "~/Scripts/iscroll-zoom.js",
                    "~/Scripts/lrz.all.bundle.js",
                    "~/Scripts/jquery.photoClip.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrapplugin").Include(
               "~/Scripts/moment-with-locales.js",
               "~/Scripts/bootstrap-datetimepicker.js",
               "~/Scripts/bootstrap-dialog.js",
               "~/Scripts/bootstrap-select.js",
               "~/Scripts/bootstrap-selsct-zh_CN.js",
               "~/Scripts/bootstrap-table.js",
               "~/Scripts/bootstrap-table-zh_CN.js",
               "~/Scripts/jquery.twbsPagination.js"));
            bundles.Add(new ScriptBundle("~/bundles/Flat-UI").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/flat-ui.js",
                    "~/Scripts/video.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/B-Admin").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/metisMenu.js",
                    "~/Scripts/sb-admin-2.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/boot-dialog").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/bootstrap-dialog.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                    "~/Scripts/Chart*"
                ));
            bundles.Add(new ScriptBundle("~/bundles/sortable").Include(
                    "~/Scripts/sortable.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                    "~/Scripts/fileinput*",
                    "~/Scripts/plugins/piexif*",
                    "~/Scripts/plugins/purify*",
                    "~/Scripts/plugins/sortable*",
                    "~/Scripts/locales/zh.js",
                    "~/Scripts/themes/fa/theme*",
                    "~/Scripts/themes/explorer-fa/theme*"

                ));
            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                    "~/Scripts/ckeditor*",
                    "~/Scripts/zh-cn.js"
                ));
            bundles.Add(new StyleBundle("~/Content/fileinput").Include(
                    "~/Content/fileinput*",
                    "~/Content/themes/explorer-fa/theme*"
                ));
            bundles.Add(new StyleBundle("~/Content/star").Include(
                    "~/Content/star-rating-svg.css"
                ));
            bundles.Add(new StyleBundle("~/Content/B-Admin").Include(
                   "~/Content/bootstrap.css",
                   "~/Content/sb-admin-2.css",
                   "~/Content/metisMenu.css",
                   "~/Content/font-awesome.css"
               ));
            bundles.Add(new StyleBundle("~/Content/Flat-UI").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/flat-ui.css"
                ));
            bundles.Add(new StyleBundle("~/Content/bootstrapplugincss").Include(
               "~/Content/bootstrap-datetimepicker.css",
               "~/Content/bootstrap-dialog.css",
               "~/Content/bootstrap-select.css",
               "~/Content/bootstrap-table.css"
               ));
            bundles.Add(new StyleBundle("~/Content/Layout").Include(
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/animate.css",
                "~/Content/bootsnav.css",
                "~/Content/overwrite.css"
                ));
            bundles.Add(new StyleBundle("~/Content/boot-dialog").Include(
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
               "~/Content/bootstrap-dialog.css"
                ));
            bundles.Add(new StyleBundle("~/Content/moviedetail").Include(
               "~/Content/bootstrap-dialog.css",
               "~/Content/lightgallery.css",
                "~/Content/moviedetail.css"
                ));
            bundles.Add(new StyleBundle("~/Content/Index").Include(
                "~/Content/immersive-slider.css",
                "~/Content/elusive-webfont.css",
                "~/Content/indexstyle.css"
                ));
            bundles.Add(new StyleBundle("~/Content/sortable").Include(
                "~/Content/sortable.css"
                ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/userinfo").Include(
                    "~/Content/coreNavigation.css",
                    "~/Content/custom.css",
                    "~/Content/typography.css"
                ));
            bundles.Add(new StyleBundle("~/Content/Logincss").Include(
                    "~/Content/normalize.min.css",
                    "~/Content/bootstrap.min.4.0.css",
                    "~/Content/Login-register-style.css",
                    "~/Content/Login-register-background-css/css/default.css",
                    "~/Content/Login-register-background-css/css/styles.css"
                ));
        }
    }
}
