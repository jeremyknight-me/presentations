using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace JK.SP.WebParts.ProviderWebPart
{
    [ToolboxItemAttribute(false)]
    public class ProviderWebPart : WebPart, IProject
    {
        protected DropDownList ProjectPicker;

        public int Id
        {
            get { return int.Parse(this.ProjectPicker.SelectedValue); }
        }

        public string Name
        {
            get { return this.ProjectPicker.SelectedItem.Text; }
        }

        protected override void CreateChildControls()
        {
            try
            {
                this.ProjectPicker = new DropDownList();

                SPWeb webSite = SPContext.Current.Web;

                SPListItemCollection listItems = webSite.Lists["Projects"].Items;
                foreach (SPListItem item in listItems)
                {
                    this.ProjectPicker.Items.Add(new ListItem(item.Title, item.ID.ToString()));
                }

                this.ProjectPicker.AutoPostBack = true;

                Controls.Add(this.ProjectPicker);
            }
            catch (Exception ex)
            {
                WebPartUtil.HandleException(this, ex);
            }
        }

        [ConnectionProvider("Project Name and ID")]
        public IProject ProvideIProject() // name does not matter
        {
            return this;
        }
    }
}
