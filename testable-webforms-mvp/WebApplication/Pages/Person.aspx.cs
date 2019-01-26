using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

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
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Person SET IsDeleted = 'True' WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter());

                command.Connection.Open();
                command.ExecuteNonQuery();
            }

            Response.Redirect("~/Pages/People.aspx");
        }

        private void LoadPerson()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Person WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter());

                command.Connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    reader.Read();
                    this.FirstNameTextBox.Text = reader["FirstName"].ToString();
                    this.LastNameTextBox.Text = reader["LastName"].ToString();
                    this.EmailTextBox.Text = reader["EmailAddress"].ToString();
                    this.PhoneTextBox.Text = reader["PhoneNumber"].ToString();
                    this.BirthdayTextBox.Text = reader["Birthday"].ToString();
                    this.NotesTextBox.Text = reader["Notes"].ToString();
                    this.CompanyDropDownList.SelectedValue = reader["CompanyId"].ToString();
                }
            }

            this.DeleteButton.Visible = true;
        }

        private void Add()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO Person ( [FirstName], [LastName], [EmailAddress], [PhoneNumber], [Birthday], [Notes], [CompanyId] ) VALUES ( @firstName, @lastName, @email, @phone, @birthday, @notes, @companyId ); SELECT SCOPE_IDENTITY();";
                this.SetupParameters(command);

                command.Connection.Open();
                object identity = command.ExecuteScalar();
                if (identity != null)
                {
                    this.PersonIdField = Convert.ToInt32(identity);
                }
            }
        }

        private void Update()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Person SET [FirstName] = @firstName, [LastName] = @lastName, [EmailAddress] = @email, [PhoneNumber] = @phone, [Birthday] = @birthday, [Notes] = @notes, [CompanyId] = @companyId WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter());
                this.SetupParameters(command);

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void SetupParameters(SqlCommand command)
        {
            command.Parameters.Add(this.BuildStringParameter("firstName", this.FirstNameTextBox));
            command.Parameters.Add(this.BuildStringParameter("lastName", this.LastNameTextBox));
            command.Parameters.Add(this.BuildStringParameter("email", this.EmailTextBox));
            command.Parameters.Add(this.BuildStringParameter("phone", this.PhoneTextBox));
            command.Parameters.Add(this.BuildBirthdayParameter());
            command.Parameters.Add(this.BuildStringParameter("notes", this.NotesTextBox));
            command.Parameters.Add(this.BuildCompanyIdParameter());
        }

        private SqlParameter BuildIdParameter()
        {
            return new SqlParameter("id", SqlDbType.Int)
            {
                Value = this.PersonIdField
            };
        }

        private SqlParameter BuildBirthdayParameter()
        {
            DateTime? birthday = null;

            DateTime date;
            if (DateTime.TryParse(this.BirthdayTextBox.Text.Trim(), out date))
            {
                birthday = date;
            }

            var parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.Date,
                ParameterName = "birthday"
            };

            if (birthday.HasValue)
            {
                parameter.Value = birthday.Value;
            }
            else
            {
                parameter.Value = DBNull.Value;
            }

            return parameter;
        }

        private SqlParameter BuildCompanyIdParameter()
        {
            string value = this.CompanyDropDownList.SelectedValue;
            int? companyId = string.IsNullOrEmpty(value) ? (int?)null : Convert.ToInt32(value);

            return new SqlParameter("companyId", SqlDbType.Int)
            {
                Value = companyId
            };
        }

        private SqlParameter BuildStringParameter(string name, ITextControl control)
        {
            string value = control.Text.Trim(); 

            var parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = name
            };

            if (string.IsNullOrEmpty(value))
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = value;
            }

            return parameter;
        }
    }
}