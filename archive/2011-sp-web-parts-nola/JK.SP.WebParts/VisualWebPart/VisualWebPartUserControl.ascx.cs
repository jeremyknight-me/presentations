using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace JK.SP.WebParts.VisualWebPart
{
    public partial class VisualWebPartUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void MyButton_Click(object sender, EventArgs e)
        {
            MyLabel.Text = "You pressed my button!";
        }
    }
}
