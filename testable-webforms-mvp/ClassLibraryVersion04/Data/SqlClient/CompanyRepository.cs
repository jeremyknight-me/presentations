using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ClassLibrary.Objects;

namespace ClassLibrary.Data.SqlClient
{
    public class CompanyRepository : ICompanyRepository
    {
        public void Add(Company company)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO Company ( Name ) VALUES ( @name ); SELECT SCOPE_IDENTITY();";
                command.Parameters.Add(this.BuildNameParameter(company.Name));

                command.Connection.Open();
                object identity = command.ExecuteScalar();
                if (identity != null)
                {
                    company.Id = Convert.ToInt32(identity);
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Company SET IsDeleted = 'True' WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter(id));

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Company> GetAll()
        {
            var companies = new List<Company>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Company] WHERE IsDeleted = 'False' ORDER BY [Name];";

                command.Connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var company = new Company
                        {
                            Id = reader.GetValueOrDefault<int>("id"),
                            Name = reader.GetValueOrDefault<string>("Name")
                        };

                        companies.Add(company);
                    }
                }
            }

            return companies;
        }

        public Company GetById(int id)
        {
            Company company;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Company WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter(id));

                command.Connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    reader.Read();

                    company = new Company
                    {
                        Id = reader.GetValueOrDefault<int>("Id"), 
                        Name = reader.GetValueOrDefault<string>("Name")
                    };
                }
            }

            return company;
        }

        public void Update(Company company)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Company SET [Name] = @name WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter(company.Id));
                command.Parameters.Add(this.BuildNameParameter(company.Name));

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private SqlParameter BuildIdParameter(int value)
        {
            return new SqlParameter("id", SqlDbType.Int)
            {
                Value = value
            };
        }

        private SqlParameter BuildNameParameter(string value)
        {
            return new SqlParameter("name", SqlDbType.NVarChar)
            {
                Value = value.Trim()
            };
        }
    }
}
