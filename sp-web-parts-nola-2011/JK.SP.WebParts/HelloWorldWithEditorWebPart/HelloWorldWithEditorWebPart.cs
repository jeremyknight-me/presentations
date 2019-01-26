using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

namespace JK.SP.WebParts.HelloWorldWithEditorWebPart
{
    [ToolboxItemAttribute(false)]
    public class HelloWorldWithEditorWebPart : WebPart
    {
        private string message;
        protected LiteralControl MessageLiteral;

        public HelloWorldWithEditorWebPart()
        {
            this.ExportMode = WebPartExportMode.All;
            this.Title = "Hello World Web Part with Editor Part";
        }

        //[System.ComponentModel.Category("Hello World Web Part Configuration")]
        //[WebDisplayName("Message")]
        //[WebDescription("Message to show to the user.")]
        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(false)]
        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(this.message))
                {
                    this.message = "Hello World! To check out my Editor Part, click Edit Web Part.";
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

        public override EditorPartCollection CreateEditorParts()
        {
            try
            {
                List<EditorPart> editorParts = new List<EditorPart>();
                editorParts.Add(new HelloWorldEditorPart(this.ID));
                return new EditorPartCollection(editorParts);
            }
            catch (Exception ex)
            {
                WebPartUtil.HandleException(this, ex);
                return base.CreateEditorParts();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            try
            {
                base.OnPreRender(e);
                this.EnsureChildControls();
                this.MessageLiteral.Text = this.Message;
            }
            catch (Exception ex)
            {
                WebPartUtil.HandleException(this, ex);
            }
        }
    }
}
