using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

namespace JK.SP.WebParts.HelloWorldWebPart
{
    [ToolboxItemAttribute(false)]
    public class HelloWorldWebPart : WebPart
    {
        private string message;
        protected LiteralControl MessageLiteral;

        public HelloWorldWebPart()
        {
            this.ExportMode = WebPartExportMode.All;
            this.Title = "Hello World Web Part";
        }

        [System.ComponentModel.Category("Hello World Web Part Configuration")]
        [WebDisplayName("Message")]
        [WebDescription("Message to show to the user.")]
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(this.message))
                {
                    this.message = "Hello World!";
                }

                return this.message;
            }
            set
            {
                this.message = value;
            }
        }

        protected override void CreateChildControls()
        {
            try
            {
                this.MessageLiteral = new LiteralControl();
                this.MessageLiteral.Text = this.Message;
                this.Controls.Add(this.MessageLiteral);
            }
            catch (Exception ex)
            {
                WebPartUtil.HandleException(this, ex);
            }
        }
    }
}
