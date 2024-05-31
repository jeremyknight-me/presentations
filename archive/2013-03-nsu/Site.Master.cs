using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class SiteMaster : MasterPage
    {
        #region Public Methods - Bottom Notification Message

        public void ShowBottomNotificationMessage(string message)
        {
            this.ShowNotificationMessage(this.BottomUserMessageLabel, message);
        }

        public void ShowBottomSuccessMessage(string message)
        {
            this.ShowSuccessMessage(this.BottomUserMessageLabel, message);
        }

        public void ShowBottomErrorMessage(string message)
        {
            this.ShowErrorMessage(this.BottomUserMessageLabel, message);
        }

        public void HideBottomMessage()
        {
            this.HideMessage(this.BottomUserMessageLabel);
        }

        public void ShowBottomNotificationMessageAjax(string message)
        {
            this.ShowBottomNotificationMessage(message);
            this.BottomUserMessageUpdatePanel.Update();
        }

        public void ShowBottomSuccessMessageAjax(string message)
        {
            this.ShowBottomSuccessMessage(message);
            this.BottomUserMessageUpdatePanel.Update();
        }

        public void ShowBottomErrorMessageAjax(string message)
        {
            this.ShowBottomErrorMessage(message);
            this.BottomUserMessageUpdatePanel.Update();
        }

        public void HideBottomMessageAjax()
        {
            this.HideBottomMessage();
            this.BottomUserMessageUpdatePanel.Update();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void ShowSuccessMessage(Label control, string message)
        {
            control.CssClass = "message-success";
            control.Text = message;
        }

        private void ShowNotificationMessage(Label control, string message)
        {
            control.CssClass = "message-notify";
            control.Text = message;
        }

        private void ShowErrorMessage(Label control, string message)
        {
            control.CssClass = "message-error";
            control.Text = message;
        }

        private void HideMessage(Label control)
        {
            control.CssClass = "hide";
            control.Text = string.Empty;
        }
    }
}
