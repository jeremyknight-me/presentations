using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace JK.SP.WebParts.ClassResourceWebPart
{
    [ToolboxItemAttribute(false)]
    public class ClassResourceWebPart : WebPart
    {
        protected Image LogoImage;

        protected override void CreateChildControls()
        {
            try
            {
                base.CreateChildControls();

                SPWeb currentWeb = SPContext.Current.Web;
                string classResourcePath 
                    = Microsoft.SharePoint.WebPartPages.SPWebPartManager.GetClassResourcePath(currentWeb, this.GetType());
                
                this.LogoImage = new Image();
                this.LogoImage.ImageUrl 
                    = string.Format("{0}/{1}", classResourcePath, "JK.SP.WebParts/ClassResources/SharePoint2010Logo.jpg");
                
                this.Controls.Add(LogoImage);
            }
            catch (Exception ex)
            {
                WebPartUtil.HandleException(this, ex);
            }
        }
    }
}
