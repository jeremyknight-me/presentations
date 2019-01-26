using System;
using WebApplication.Code;
using WebApplication.Code.Logic;
using WebApplication.Code.Objects;

namespace WebApplication.Pages
{
    public partial class CompanyDetails : PageBase
    {
        private const string companyIdKey = "CompanyId";

        protected int CompanyId
        {
            get
            {
                object obj = this.ViewState[companyIdKey];
                return obj == null ? 0 : Convert.ToInt32(this.ViewState[companyIdKey]);
            }

            set
            {
                this.ViewState[companyIdKey] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.IsQueryStringValid("id0"))
                {
                    int companyId = Convert.ToInt32(this.Request.QueryString.Get("id0"));
                    this.PageHeaderLiteral.Text = "Edit Company Details";
                    this.LoadCompany(companyId);
                }
                else
                {
                    this.PageHeaderLiteral.Text = "Add Company";
                }

                this.PageHeaderLiteral.EnableViewState = false;
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var service = new CompanyService();
            Company company = this.GetCompanyFromView();

            try
            {
                if (this.CompanyId != 0)
                {
                    company.Id = this.CompanyId;
                    service.Update(company);
                }
                else
                {
                    service.Add(company);
                }

                this.Master.ShowBottomSuccessMessageAjax("Company successfully saved.");
            }
            catch (Exception ex)
            {
                this.Master.ShowBottomErrorMessageAjax("Error occurred while trying to save company.");
            }
        }

        private Company GetCompanyFromView()
        {
            return new Company
                {
                    Name = this.GetTextControlValue(this.CompanyNameTextBox)
                };
        }

        private void LoadCompany(int companyId)
        {
            var service = new CompanyService();
            Company company = service.GetById(companyId);
            this.CompanyId = company.Id;
            this.SetTextControlValue(this.CompanyNameTextBox, company.Name);
        }
    }
}