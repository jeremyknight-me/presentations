using System;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.WebPartPages;

namespace JK.SP.WebParts.HelloWorldWithEditorWebPart
{
    public class HelloWorldEditorPart : EditorPart
    {
        protected TextBox MessageTextBox;
        protected ValidationSummary ValidationSummary;

        public HelloWorldEditorPart(string id)
        {
            // editor parts must have unique id per web part to ensure that multiple
            // web parts of the same type can be used on a page
            ID = string.Format("HelloWorldEditorPart_{0}", id);
            Title = "Hello World Options";
        }

        protected override void CreateChildControls()
        {
            try
            {
                base.CreateChildControls();

                Panel pnlGroup = new Panel(); // configuration section panel

                ValidationSummary = new ValidationSummary();
                ValidationSummary.ID = "ValidationSummary";
                ValidationSummary.DisplayMode = ValidationSummaryDisplayMode.SingleParagraph;
                ValidationSummary.ShowSummary = true;
                pnlGroup.Controls.Add(ValidationSummary);

                MessageTextBox = new TextBox();
                MessageTextBox.ID = "MessageTextBox";

                HelloWorldWithEditorWebPart webPart 
                    = this.WebPartToEdit as HelloWorldWithEditorWebPart;

                if (webPart != null)
                {
                    Label lblMessage = new Label();
                    lblMessage.Text = "Message";

                    Panel pnlMessageLabel = new Panel(); // head for configuration section
                    pnlMessageLabel.Controls.Add(lblMessage);

                    // Required field validator
                    RequiredFieldValidator messageRequiredValidator =
                        new RequiredFieldValidator();
                    messageRequiredValidator.ControlToValidate = MessageTextBox.ClientID;
                    messageRequiredValidator.ErrorMessage = "Message cannot be blank";
                    messageRequiredValidator.Display = ValidatorDisplay.Dynamic;
                    messageRequiredValidator.Text = "*";

                    Panel pnlMessageControls = new Panel(); // body of configuration section
                    pnlMessageControls.Controls.Add(MessageTextBox);
                    pnlMessageControls.Controls.Add(messageRequiredValidator);

                    pnlGroup.Controls.Add(pnlMessageLabel);
                    pnlGroup.Controls.Add(pnlMessageControls);
                }

                this.Controls.Add(pnlGroup);

                // stop Cancel button from calling validation code
                ToolPane pane = Zone as ToolPane;
                if (pane != null)
                    pane.Cancel.CausesValidation = false;
            }
            catch (Exception ex)
            {
                WebPartUtil.HandleException(this, ex);
            }
        }

        /// <summary>
        /// Places data in editor part properties/controls into web part properties.
        /// </summary>
        public override bool ApplyChanges()
        {
            EnsureChildControls();

            try
            {
                HelloWorldWithEditorWebPart webPart
                    = this.WebPartToEdit as HelloWorldWithEditorWebPart;

                if (webPart != null)
                {
                    webPart.Message = MessageTextBox.Text;
                }
            }
            catch (Exception ex)
            {
                WebPartUtil.HandleException(this, ex);
            }

            return true;
        }

        /// <summary>
        /// Places data in web part properties into editor part properties/controls
        /// </summary>
        public override void SyncChanges()
        {
            EnsureChildControls();

            try
            {
                HelloWorldWithEditorWebPart webPart 
                    = this.WebPartToEdit as HelloWorldWithEditorWebPart;
                if (webPart != null)
                {
                    MessageTextBox.Text = webPart.Message;
                }
            }
            catch (Exception ex)
            {
                WebPartUtil.HandleException(this, ex);
            }
        }
    }
}
