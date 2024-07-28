using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Company SET IsDeleted = 'True' WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter());

                command.Connection.Open();
                command.ExecuteNonQuery();
            }

            Response.Redirect("~/Pages/Companies.aspx");
        }

        private void LoadCompany()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Company WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter());

                command.Connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    reader.Read();
                    this.NameTextBox.Text = reader["Name"].ToString();
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
                command.CommandText = "INSERT INTO Company ( Name ) VALUES ( @name ); SELECT SCOPE_IDENTITY();";
                command.Parameters.Add(this.BuildNameParameter());

                command.Connection.Open();
                object identity = command.ExecuteScalar();
                if (identity != null)
                {
                    this.CompanyIdField = Convert.ToInt32(identity);
                }
            }
        }

        private void Update()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Company SET [Name] = @name WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter());
                command.Parameters.Add(this.BuildNameParameter());

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private SqlParameter BuildIdParameter()
        {
            return new SqlParameter("id", SqlDbType.Int)
            {
                Value = this.CompanyIdField
            };
        }

        private SqlParameter BuildNameParameter()
        {
            return new SqlParameter("name", SqlDbType.NVarChar)
            {
                Value = this.NameTextBox.Text.Trim()
            };
        }
    }
}