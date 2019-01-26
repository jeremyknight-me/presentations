using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ClassLibrary.Presentation.Presenters;
using ClassLibrary.Presentation.ViewContracts;

namespace WebApplication.Pages
{
    public partial class Person : PageBase, IPersonDetailView
    {
        private const string viewStatePersonIdkey = "PersonId";

        public int PersonIdField
        {
            get { return Convert.ToInt32(this.ViewState[viewStatePersonIdkey]); }
            set { this.ViewState[viewStatePersonIdkey] = value; }
        }

        public string FirstNameField
        {
            get { return this.FirstNameTextBox.Text.Trim(); }
            set { this.FirstNameTextBox.Text = value; }
        }

        public string LastNameField
        {
            get { return this.LastNameTextBox.Text.Trim(); }
            set { this.LastNameTextBox.Text = value; }
        }

        public string EmailAddressField
        {
            get { return this.EmailTextBox.Text.Trim(); }
            set { this.EmailTextBox.Text = value; }
        }

        public string PhoneNumberField
        {
            get { return this.PhoneTextBox.Text.Trim(); }
            set { this.PhoneTextBox.Text = value; }
        }

        public string NotesField
        {
            get { return this.NotesTextBox.Text.Trim(); }
            set { this.NotesTextBox.Text = value; }
        }

        public DateTime? BirthdayField
        {
            get
            {
                DateTime date;
                if (DateTime.TryParse(this.BirthdayTextBox.Text.Trim(), out date))
                {
                    return date;
                }

                return null;
            }
            set
            {
                this.BirthdayTextBox.Text = value.HasValue
                    ? value.Value.ToShortDateString()
                    : string.Empty;
            }
        }

        public int? CompanyIdField
        {
            get
            {
                string value = this.CompanyDropDownList.SelectedValue;
                int? companyId = string.IsNullOrEmpty(value) ? (int?)null : Convert.ToInt32(value);
                return companyId;
            }
            set
            {
                this.CompanyDropDownList.SelectedValue = value.HasValue ? value.Value.ToString() : string.Empty;
            }
        }

        public IEnumerable<ClassLibrary.Objects.Company> CompanyFieldItems
        {
            set
            {
                this.CompanyDropDownList.DataSource = value;
                this.CompanyDropDownList.DataBind();
                this.CompanyDropDownList.Items.Insert(0, new ListItem("- Select Company -", string.Empty));
            }
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
                    this.PersonIdField = Convert.ToInt32(this.Request.QueryString.Get("id0"));
                }

                var presenter = new PersonDetailPresenter(this);
                presenter.Load();
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var presenter = new PersonDetailPresenter(this);
            presenter.Save();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            var presenter = new PersonDetailPresenter(this);
            presenter.Delete();
            Response.Redirect("~/Pages/People.aspx");
        }
    }
}