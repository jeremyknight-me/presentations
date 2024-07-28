using System;
using ClassLibrary.Data.SqlClient;

namespace WebApplication.Pages
{
    public partial class Company : PageBase
    {
        private const string viewStateCompanyIdkey = "CompanyId";

        public int CompanyIdField
        {
            get { return Convert.ToInt32(this.ViewState[viewStateCompanyIdkey]); }
            set { this.ViewState[viewStateCompanyIdkey] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.IsQueryStringValid("id0"))
                {
                    this.CompanyIdField = Convert.ToInt32(this.Request.QueryString.Get("id0"));
                    this.LoadCompany();
                }
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.CompanyIdField > 0)
            {
                this.Update();
            }
            else
            {
                this.Add();
            }

            this.DeleteButton.Visible = true;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            var repository = new CompanyRepository();
            repository.Delete(this.CompanyIdField);
            Response.Redirect("~/Pages/Companies.aspx");
        }

        private void LoadCompany()
        {
            var repository = new CompanyRepository();
            ClassLibrary.Objects.Company company = repository.GetById(this.CompanyIdField);
            this.NameTextBox.Text = company.Name;
            this.DeleteButton.Visible = true;
        }

        private void Add()
        {
            var company = new ClassLibrary.Objects.Company { Name = this.NameTextBox.Text.Trim() };
            var repository = new CompanyRepository();
            repository.Add(company);
            this.CompanyIdField = company.Id;
        }

        private void Update()
        {
            var company = new ClassLibrary.Objects.Company
            {
                Id = this.CompanyIdField,
                Name = this.NameTextBox.Text.Trim()
            };
            var repository = new CompanyRepository();
            repository.Update(company);
        }
    }
}