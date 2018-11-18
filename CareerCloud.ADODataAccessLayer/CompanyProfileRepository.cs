using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    class CompanyProfileRepository : BaseADO, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach(CompanyProfilePoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Profiles] ([Id],[Registration_Date],[Company_Website],[Contact_Phone],[Contact_Name],[Company_Logo]
                    VALUES (@Id, @RegistrationDate, @CompanyWebsite, @ContactPhone, @ContactName, @CompanyLogo)";

                    command.Parameters.AddWithValue(@"Id", poco.Id);
                    command.Parameters.AddWithValue(@"RegistrationDate", poco.RegistrationDate);
                    command.Parameters.AddWithValue(@"CompanyWebsite", poco.CompanyWebsite);
                    command.Parameters.AddWithValue(@"ContactPhone", poco.ContactPhone);
                    command.Parameters.AddWithValue(@"ContactName", poco.ContactName);
                    command.Parameters.AddWithValue(@"CompanyLogo", poco.CompanyLogo);

                    conn.Open();
                    int rowEffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[500];

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("Select * from Company_Profiles", conn);
                int position = 0;

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();

                    poco.Id = reader.GetGuid(0);
                    poco.RegistrationDate = reader.GetDateTime(1);
                    poco.CompanyWebsite = reader.GetString(2);
                    poco.ContactPhone = reader.GetString(3);
                    poco.ContactName = reader.GetString(4);
                    poco.CompanyLogo = (byte[])reader[5];
                    poco.TimeStamp = (byte[])reader[6];

                    pocos[position] = poco;
                    position++;

                }
            }
            return pocos.ToList();
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            throw new NotImplementedException();
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            throw new NotImplementedException();
        }
    }
}
