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
            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                    "~/Scripts/Chart*"
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
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
