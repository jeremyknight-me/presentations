using System;
using ClassLibrary.Data.SqlClient;

namespace WebApplication.Pages
{
    public partial class Person : PageBase
    {
        private const string viewStatePersonIdkey = "PersonId";

        public int PersonIdField
        {
            get { return Convert.ToInt32(this.ViewState[viewStatePersonIdkey]); }
            set { this.ViewState[viewStatePersonIdkey] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.IsQueryStringValid("id0"))
                {
                    this.PersonIdField = Convert.ToInt32(this.Request.QueryString.Get("id0"));
                    this.LoadPerson();
                }
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.PersonIdField > 0)
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
            var repository = new PersonRepository();
            repository.Delete(this.PersonIdField);
            Response.Redirect("~/Pages/People.aspx");
        }

        private void LoadPerson()
        {
            var repository = new PersonRepository();
            ClassLibrary.Objects.Person person = repository.GetById(this.PersonIdField);

            this.FirstNameTextBox.Text = person.FirstName;
            this.LastNameTextBox.Text = person.LastName;
            this.EmailTextBox.Text = person.EmailAddress;
            this.PhoneTextBox.Text = person.PhoneNumber;
            this.BirthdayTextBox.Text = person.Birthday.HasValue
                ? person.Birthday.Value.ToShortDateString()
                : string.Empty;
            this.NotesTextBox.Text = person.Notes;
            this.CompanyDropDownList.SelectedValue = person.CompanyId.ToString();

            this.DeleteButton.Visible = true;
        }

        private void Add()
        {
            ClassLibrary.Objects.Person person = this.GetPersonFromView();
            var repository = new PersonRepository();
            repository.Add(person);
            this.PersonIdField = person.Id;
        }

        private void Update()
        {
            ClassLibrary.Objects.Person person = this.GetPersonFromView();
            var repository = new PersonRepository();
            repository.Update(person);
        }

        private ClassLibrary.Objects.Person GetPersonFromView()
        {
            return new ClassLibrary.Objects.Person
            {
                Id = this.PersonIdField,
                FirstName = this.FirstNameTextBox.Text.Trim(),
                LastName = this.LastNameTextBox.Text.Trim(),
                EmailAddress = this.EmailTextBox.Text.Trim(),
                PhoneNumber = this.PhoneTextBox.Text.Trim(),
                Notes = this.NotesTextBox.Text.Trim(),
                Birthday = this.GetPersonBirthdayFromView(),
                CompanyId = this.GetCompanyIdFromView()
            };
        }

        private DateTime? GetPersonBirthdayFromView()
        {
            DateTime date;
            if (DateTime.TryParse(this.BirthdayTextBox.Text.Trim(), out date))
            {
                return date;
            }

            return null;
        }

        private int? GetCompanyIdFromView()
        {
            string value = this.CompanyDropDownList.SelectedValue;
            int? companyId = string.IsNullOrEmpty(value) ? (int?)null : Convert.ToInt32(value);
            return companyId;
        }
    }
}