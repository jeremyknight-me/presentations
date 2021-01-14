using System;
using System.Web.UI.WebControls;
using WebApplication.Code;
using WebApplication.Code.Logic;

namespace WebApplication.Pages
{
    public partial class People : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadPeople();
            }
        }

        protected void PersonListView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                int personId = Convert.ToInt32(e.Keys[0]);
                var service = new PersonService();
                service.Delete(personId);
                this.LoadPeople();
                this.Master.ShowBottomNotificationMessageAjax("Person successfully removed.");
            }
            catch (Exception ex)
            {
                const string message = "Error occurred while trying to delete person.";
                this.Master.ShowBottomErrorMessageAjax(message);
            }
            
        }

        private void LoadPeople()
        {
            var service = new PersonService();
            this.PersonListView.DataSource = service.GetAll();
            this.PersonListView.DataBind();
        }
    }
}