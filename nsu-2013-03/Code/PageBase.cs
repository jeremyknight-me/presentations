using System;
using System.Web.UI;

namespace WebApplication.Code
{
    public abstract class PageBase : Page
    {
        protected string ApplicationTitle
        {
            get { return Properties.Settings.Default.ApplicationTitle; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.MaintainScrollPositionOnPostBack = true;

            if (!this.DesignMode
                && !this.IsPostBack)
            {
                this.Title = this.GetTitle();
            }
        }

        protected string GetTextControlValue(ITextControl textControl)
        {
            return textControl.Text.Trim();
        }

        protected void SetTextControlValue(ITextControl textControl, string value)
        {
            textControl.Text = value;
        }

        protected bool IsQueryStringValid(string key)
        {
            return this.Request.QueryString[key] != null
                   && !string.IsNullOrEmpty(this.Request.QueryString[key]);
        }

        private string GetTitle()
        {
            string pageTitle = this.Title;

            return this.IsPageTitleEmpty()
                ? this.ApplicationTitle
                : string.Concat(pageTitle, " | ", this.ApplicationTitle);
        }

        private bool IsPageTitleEmpty()
        {
            return string.IsNullOrEmpty(this.Title);
        }
    }
}