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
using System.Web.UI.WebControls;

namespace BlogEngine.NET.Custom.Extensions
{
    /// <summary>
    /// Class for adding Actions to execute at different states of the render pipeline.
    /// </summary>
    public sealed class Pipeline
    {
        private static List<Action<object, EventArgs>> _actionsOnLoad;

        /// <summary>
        /// Actions to be triggerd in Page_Load() in Global.asax
        /// </summary>
        public static List<Action<object, EventArgs>> ActionsOnLoad { get { return _actionsOnLoad; } }

        /// <summary>
        /// Ctor
        /// </summary>
        static Pipeline()
        {
            _actionsOnLoad = new List<Action<object, EventArgs>>();
        }

        /// <summary>
        /// Actions added will be execute Page_Load() in Global.asax
        /// </summary>
        /// <param name="action"></param>
        public static void ExecuteOnPageLoad(Action<object, EventArgs> action)
        {
            _actionsOnLoad.Add(action);
        }
    }

    //TODO CSS Default CSS should be configurable!
    [Extension("CSS Switch", "1.0", @"<a href=""http://ancientbyte.software/"">AncientGrief</a>")]
    public class CSSSwitch
    {
        public CSSSwitch() 
        {
            Pipeline.ExecuteOnPageLoad(OnPageLoad);
        }

        private void OnPageLoad(object sender, EventArgs evt)
        {
            MasterPage page = (MasterPage)sender;
            HttpContext context = HttpContext.Current;

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

        private static void SetCSS(MasterPage page, string cssFile)
        {
            ContentPlaceHolder c = page.FindControl("HeadContent") as ContentPlaceHolder;
            if (c != null)
            {
                LiteralControl ltr = new LiteralControl();

                string path = string.Format("{0}Custom/Themes/{1}/css/{2}", Utils.ApplicationRelativeWebRoot, BlogSettings.Instance.GetThemeWithAdjustments(null), cssFile);
                ltr.Text = string.Format("<link  type=\"text/css\" rel=\"stylesheet\" href=\"{0}\"></link>", path);
                c.Controls.Add(ltr);
            }
        }
    }
}