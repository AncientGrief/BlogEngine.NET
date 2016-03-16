using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.HtmlControls;
using BlogEngine.Core;
using BlogEngine.Core.Web.Controls;
using BlogEngine.Core.Web.Extensions;
using Page = System.Web.UI.Page;
using System.Web.UI;

namespace BlogEngine.NET.Custom.Extensions
{
    //TODO CSS Default CSS should be configurable!
    [Extension("CSS Switch", "1.0", @"<a href=""http://ancientbyte.software/"">AncientGrief</a>")]
    public class CSSSwitch
    {
        public CSSSwitch()
        {
            Post.Serving += Publishable_Serving;
            BlogEngine.Core.Page.Serving += Publishable_Serving;
        }

        private void Publishable_Serving(object sender, ServingEventArgs e)
        {
            if (!ExtensionManager.ExtensionEnabled("CSSSwitch"))
                return;
            HttpContext context = HttpContext.Current;

            if (context.CurrentHandler is Page)
            {
                Page page = (Page)context.CurrentHandler;
                string cssFile = "mainRed.css";

                var cookie = HttpContext.Current.Request.Cookies["blogEngine_cssswitch"];
                if (cookie != null)
                {
                    cssFile = cookie.Value;
                }
                else
                {
                    HttpCookie myCookie = new HttpCookie("blogEngine_cssswitch");
                    DateTime now = DateTime.Now;

                    // Set the cookie value.
                    myCookie.Value = cssFile;
                    // Set the cookie expiration date.
                    myCookie.Expires = now.AddYears(10);
                    
                    // Add the cookie.
                    context.Response.Cookies.Add(myCookie);
                }

                SetCSS(page, cssFile);
            }
        }

        private static void SetCSS(Page page, string cssFile)
        {
            LiteralControl ltr = new LiteralControl();

            string path = string.Format("{0}Custom/Themes/{1}/css/{2}", Utils.ApplicationRelativeWebRoot, BlogSettings.Instance.GetThemeWithAdjustments(null), cssFile);
            ltr.Text = string.Format("<link  type=\"text/css\" rel=\"stylesheet\" href=\"{0}\"></link>", path);

            page.Header.Controls.Add(ltr);
        }
    }
}