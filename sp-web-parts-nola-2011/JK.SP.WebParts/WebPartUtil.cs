using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JK.SP.WebParts
{
    internal static class WebPartUtil
    {
        internal static void HandleException(WebControl control, Exception ex)
        {
            control.Controls.Clear();
            control.Controls.Add(new LiteralControl(ex.Message));
        }
    }
}
