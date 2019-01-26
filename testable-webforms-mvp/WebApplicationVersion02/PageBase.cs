using System;
using System.Web.UI;

namespace WebApplication
{
    public class PageBase : Page
    {
        private const string applicationTitle = "Contact Tracking";

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.MaintainScrollPositionOnPostBack = true;

            if (!this.DesignMode && !this.IsPostBack)
            {
                string pageTitle = this.Title;
                this.Title = pageTitle == applicationTitle || string.IsNullOrEmpty(pageTitle)
                    ? applicationTitle
                    : string.Concat(pageTitle, " - ", applicationTitle);
            }
        }

        protected bool IsQueryStringValid(string key)
        {
            if (this.Request.QueryString[key] != null
                && !string.IsNullOrEmpty(this.Request.QueryString[key]))
            {
                return true;
            }

            return false;
        }
    }
}