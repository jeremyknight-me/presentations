using System;
using ClassLibrary.Presentation.Presenters;
using ClassLibrary.Presentation.ViewContracts;

namespace WebApplication.Pages
{
    public partial class Company : PageBase, ICompanyDetailView
    {
        private const string viewStateCompanyIdkey = "CompanyId";

        public int CompanyIdField
        {
            get { return Convert.ToInt32(this.ViewState[viewStateCompanyIdkey]); }
            set { this.ViewState[viewStateCompanyIdkey] = value; }
        }

        public string NameField
        {
            get { return this.NameTextBox.Text.Trim(); } 
            set { this.NameTextBox.Text = value; }
        }

        public bool CanUserDelete
        {
            set { this.DeleteButton.Visible = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.IsQueryStringValid("id0"))
                {
                    this.CompanyIdField = Convert.ToInt32(this.Request.QueryString.Get("id0"));
                }

                var presenter = new CompanyDetailPresenter(this);
                presenter.Load();
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var presenter = new CompanyDetailPresenter(this);
            presenter.Save();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            var presenter = new CompanyDetailPresenter(this);
            presenter.Delete();
            Response.Redirect("~/Pages/Companies.aspx");
        }
    }
}