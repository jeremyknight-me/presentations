using System;
using System.Web.UI.WebControls;
using WebApplication.Code;
using WebApplication.Code.Logic;

namespace WebApplication.Pages
{
    public partial class Companies : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadCompanies();    
            }
        }

        protected void CompanyListView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                int companyId = Convert.ToInt32(e.Keys[0]);
                var service = new CompanyService();
                service.Delete(companyId);
                this.LoadCompanies();
                this.Master.ShowBottomNotificationMessageAjax("Company successfully deleted.");
            }
            catch (Exception ex)
            {
                const string message = "Error occurred while trying to delete company.";
                this.Master.ShowBottomErrorMessageAjax(message);
            }
        }

        private void LoadCompanies()
        {
            var service = new CompanyService();
            this.CompanyListView.DataSource = service.GetAll();
            this.CompanyListView.DataBind();
        }
    }
}