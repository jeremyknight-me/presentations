using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace JK.SP.WebParts.ConsumerWebPart
{
    [ToolboxItemAttribute(false)]
    public class ConsumerWebPart : WebPart
    {
        private IProject provider = null;
        protected Label DisplayLabel = null;

        protected override void CreateChildControls()
        {
            try
            {
                this.DisplayLabel = new Label();

                if (this.provider != null)
                {
                    if (this.provider.Id > 0)
                    {
                        this.DisplayLabel.Text = string.Format("'{0}' was selected.", this.provider.Name);
                    }
                    else
                    {
                        this.DisplayLabel.Text = "Nothing was selected.";
                    }
                }
                else
                {
                    this.DisplayLabel.Text = "No Provider Web Part Connected.";
                }

                Controls.Add(this.DisplayLabel);
            }
            catch (Exception ex)
            {
                WebPartUtil.HandleException(this, ex);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            EnsureChildControls();
        }

        [ConnectionConsumer("Project Name and ID")]
        public void ConsumeIProject(IProject providerInterface) // method name does not matter
        {
            this.provider = providerInterface;
        }
    }
}
