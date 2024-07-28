using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Code;
using WebApplication.Code.Logic;
using WebApplication.Code.Objects;

namespace WebApplication.Pages
{
    public partial class PersonDetails : PageBase
    {
        private const string personIdKey = "PersonId";

        protected int PersonId
        {
            get
            {
                object obj = this.ViewState[personIdKey];
                return obj == null ? 0 : Convert.ToInt32(this.ViewState[personIdKey]);
            }

            set
            {
                this.ViewState[personIdKey] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.SetupExtenders(Properties.Settings.Default.UseAjaxExtenders);
                this.LoadCompanies();
                this.LoadStates();

                if (this.IsQueryStringValid("id0"))
                {
                    int personId = Convert.ToInt32(this.Request.QueryString.Get("id0"));
                    this.PageHeaderLiteral.Text = "Edit Person Details";
                    this.LoadPerson(personId);
                }
                else
                {
                    this.PageHeaderLiteral.Text = "Add Person";
                }

                this.PageHeaderLiteral.EnableViewState = false;
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new PersonService();
                Person person = this.GetPersonFromView();

                if (this.PersonId != 0)
                {
                    person.Id = this.PersonId;
                    service.Update(person);
                }
                else
                {
                    service.Add(person);
                }

                this.Master.ShowBottomSuccessMessageAjax("Person successfully saved.");
            }
            catch (Exception ex)
            {
                const string message = "Error occurred while trying to save person. ";
                this.Master.ShowBottomErrorMessageAjax(message + ex.Message);
            }
        }

        private void LoadStates()
        {
            var service = new StateService();
            IEnumerable<ListItemData> list = service.GetAll();

            DropDownList control = this.StateDropDownList;
            control.DataSource = list;
            control.DataBind();
        }

        private void LoadCompanies()
        {
            var service = new CompanyService();
            IEnumerable<Company> list = service.GetAll();

            DropDownList control = this.CompanyDropDownList;
            control.DataSource = list;
            control.DataBind();
        }

        private void LoadPerson(int personId)
        {
            var service = new PersonService();
            Person person = service.GetById(personId);
            this.PersonId = person.Id;
            this.SetTextControlValue(this.BirthdayTextBox, person.BirthdayDisplay);
            this.SetTextControlValue(this.CityTextBox, person.City);
            this.CompanyDropDownList.SelectedValue = person.CompanyId.ToString();
            this.SetTextControlValue(this.EmailTextBox, person.EmailAddress);
            this.SetTextControlValue(this.FirstNameTextBox, person.FirstName);
            this.SetTextControlValue(this.LastNameTextBox, person.LastName);
            this.SetTextControlValue(this.MiddleNameTextBox, person.MiddleName);
            this.SetTextControlValue(this.NicknameTextBox, person.Nickname);
            this.SetTextControlValue(this.PhoneNumberTextBox, person.PhoneNumber);
            this.StateDropDownList.SelectedValue = person.State;
            this.SetTextControlValue(this.StreetAddressTextBox, person.Street);
            this.SetTextControlValue(this.ZipCodeTextBox, person.ZipCode);
        }

        private Person GetPersonFromView()
        {
            var person = new Person
                {
                    Birthday = this.GetNullableDateTimeFromTextControl(this.BirthdayTextBox),
                    City = this.GetTextControlValue(this.CityTextBox),
                    CompanyId = Convert.ToInt32(this.CompanyDropDownList.SelectedValue),
                    EmailAddress = this.GetTextControlValue(this.EmailTextBox),
                    FirstName = this.GetTextControlValue(this.FirstNameTextBox),
                    LastName = this.GetTextControlValue(this.LastNameTextBox),
                    MiddleName = this.GetTextControlValue(this.MiddleNameTextBox),
                    Nickname = this.GetTextControlValue(this.NicknameTextBox),
                    PhoneNumber = this.GetTextControlValue(this.PhoneNumberTextBox),
                    State = this.StateDropDownList.SelectedValue,
                    Street = this.GetTextControlValue(this.StreetAddressTextBox),
                    ZipCode = this.GetTextControlValue(this.ZipCodeTextBox)
                };

            return person;
        }

        private DateTime? GetNullableDateTimeFromTextControl(ITextControl control)
        {
            if (string.IsNullOrEmpty(control.Text))
            {
                return null;
            }

            DateTime value;
            if (DateTime.TryParse(control.Text, out value))
            {
                return value;
            }

            return null;
        }

        private void SetupExtenders(bool areUsed)
        {
            this.FirstNameTextBoxWatermarkExtender.Enabled = areUsed;
            this.FirstNameFilteredTextBoxExtender.Enabled = areUsed;
            this.MiddleNameTextBoxWatermarkExtender.Enabled = areUsed;
            this.MiddleNameFilteredTextBoxExtender.Enabled = areUsed;
            this.LastNameTextBoxWatermarkExtender.Enabled = areUsed;
            this.LastNameFilteredTextBoxExtender.Enabled = areUsed;
            this.NicknameTextBoxWatermarkExtender.Enabled = areUsed;
            this.NicknameFilteredTextBoxExtender.Enabled = areUsed;
            this.EmailTextBoxWatermarkExtender.Enabled = areUsed;
            this.PhoneNumberTextBoxWatermarkExtender.Enabled = areUsed;
            this.PhoneNumberMaskedEditExtender.Enabled = areUsed;
            this.StreetTextBoxWatermarkExtender.Enabled = areUsed;
            this.CityTextBoxWatermarkExtender.Enabled = areUsed;
            this.ZipCodeTextBoxWatermarkExtender.Enabled = areUsed;
            this.BirthdayTextBoxWatermarkExtender.Enabled = areUsed;
            this.BirthdayMaskedEditExtender.Enabled = areUsed;
            this.BirthdayCalendarExtender.Enabled = areUsed;
        }
    }
}