using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ClassLibrary.Objects;

namespace ClassLibrary.Data.SqlClient
{
    public class PersonRepository : IPersonRepository
    {
        public void Add(Person person)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO Person ( [FirstName], [LastName], [EmailAddress], [PhoneNumber], [Birthday], [Notes], [CompanyId] ) VALUES ( @firstName, @lastName, @email, @phone, @birthday, @notes, @companyId ); SELECT SCOPE_IDENTITY();";
                this.SetupParameters(command, person);

                command.Connection.Open();
                object identity = command.ExecuteScalar();
                if (identity != null)
                {
                    person.Id = Convert.ToInt32(identity);
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Person SET IsDeleted = 'True' WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter(id));

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Person> GetAll()
        {
            var people = new List<Person>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Person] WHERE IsDeleted = 'False' ORDER BY [LastName], [FirstName];";

                command.Connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var person = this.LoadPersonFromDataRecord(reader);
                        person.Id = reader.GetValueOrDefault<int>("Id");
                        people.Add(person);    
                    }
                }
            }

            return people;
        }

        public IEnumerable<Person> GetByCompanyId(int companyId)
        {
            var people = new List<Person>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM [Person] WHERE IsDeleted = 'False' AND [CompanyId] = @companyId ORDER BY [LastName], [FirstName];";

                var parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = "companyId",
                    Value = companyId
                };
                command.Parameters.Add(parameter);

                command.Connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var person = this.LoadPersonFromDataRecord(reader);
                        person.Id = reader.GetValueOrDefault<int>("Id");
                        people.Add(person);
                    }
                }
            }

            return people;
        }

        public Person GetById(int id)
        {
            Person person;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Person WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter(id));

                command.Connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    reader.Read();
                    person = this.LoadPersonFromDataRecord(reader);
                }
            }

            return person;
        }

        public void Update(Person person)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataStore"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Person SET [FirstName] = @firstName, [LastName] = @lastName, [EmailAddress] = @email, [PhoneNumber] = @phone, [Birthday] = @birthday, [Notes] = @notes, [CompanyId] = @companyId WHERE Id = @id;";
                command.Parameters.Add(this.BuildIdParameter(person.Id));
                this.SetupParameters(command, person);

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private Person LoadPersonFromDataRecord(IDataRecord record)
        {
            return new Person
            {
                FirstName = record.GetValueOrDefault<string>("FirstName"),
                LastName = record.GetValueOrDefault<string>("LastName"),
                EmailAddress = record.GetValueOrDefault<string>("EmailAddress"),
                PhoneNumber = record.GetValueOrDefault<string>("PhoneNumber"),
                Notes = record.GetValueOrDefault<string>("Notes"),
                Birthday = record.GetValueOrNull<DateTime?>("Birthday"),
                CompanyId = record.GetValueOrNull<int?>("CompanyId")
            };
        }

        private void SetupParameters(SqlCommand command, Person person)
        {
            command.Parameters.Add(this.BuildStringParameter("firstName", person.FirstName));
            command.Parameters.Add(this.BuildStringParameter("lastName", person.LastName));
            command.Parameters.Add(this.BuildStringParameter("email", person.EmailAddress));
            command.Parameters.Add(this.BuildStringParameter("phone", person.PhoneNumber));
            command.Parameters.Add(this.BuildStringParameter("notes", person.Notes));
            command.Parameters.Add(this.BuildBirthdayParameter(person.Birthday));
            command.Parameters.Add(this.BuildCompanyIdParameter(person.CompanyId));
        }

        private SqlParameter BuildIdParameter(int value)
        {
            return new SqlParameter("id", SqlDbType.Int)
            {
                Value = value
            };
        }

        private SqlParameter BuildStringParameter(string name, string value)
        {
            string trimmed = value.Trim();

            var parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = name
            };

            if (string.IsNullOrEmpty(trimmed))
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = trimmed;
            }

            return parameter;
        }

        private SqlParameter BuildBirthdayParameter(DateTime? birthday)
        {
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

        private SqlParameter BuildCompanyIdParameter(int? companyId)
        {
            var parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "companyId"
            };

            if (companyId.HasValue)
            {
                parameter.Value = companyId.Value;
            }
            else
            {
                parameter.Value = DBNull.Value;
            }

            return parameter;
        }
    }
}
