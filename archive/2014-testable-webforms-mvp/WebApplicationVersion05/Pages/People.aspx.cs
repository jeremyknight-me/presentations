using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ClassLibrary.Presentation.Presenters;
using ClassLibrary.Presentation.ViewContracts;

namespace WebApplication.Pages
{
    public partial class People : PageBase, IPersonListView
    {
        public int? SelectedCompanyIdField
        {
            get
            {
                string value = this.FilterCompanyDropDownList.SelectedValue;
                int? companyId = string.IsNullOrEmpty(value) ? (int?)null : Convert.ToInt32(value);
                return companyId;
            }
            set 
            {
                this.FilterCompanyDropDownList.SelectedValue = value.HasValue 
                    ? value.Value.ToString() 
                    : string.Empty;
            }
        }

        public IEnumerable<ClassLibrary.Objects.Company> CompanyFilterFieldItems
        {
            set
            {
                this.FilterCompanyDropDownList.DataSource = value;
                this.FilterCompanyDropDownList.DataBind();
                this.FilterCompanyDropDownList.Items.Insert(0, new ListItem("- Select Company -", string.Empty));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var presenter = new PersonListPresenter(this);
                presenter.Load();
            }
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            this.BindListView();
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            var presenter = new PersonListPresenter(this);
            presenter.Clear();
            this.BindListView();
        }

        protected void PersonObjectDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            var presenter = new PersonListPresenter(this);
            e.ObjectInstance = presenter;
        }

        private void BindListView()
        {
            this.ListViewDataPager.SetPageProperties(0, this.ListViewDataPager.MaximumRows, true);
        }
    }
}