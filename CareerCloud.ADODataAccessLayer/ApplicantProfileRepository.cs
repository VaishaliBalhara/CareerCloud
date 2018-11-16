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
    class ApplicantProfileRepository : BaseADO, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach(ApplicantProfilePoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]([Id],[Login],[Current_Salary],[Current_Rate],[Currency],[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code]
                    VALUES (@Id, @Login, @Current_Salary, @CurrentRate, @Currency, @Country, @Province, @Street, @City, @PostalCode)";
                    command.Parameters.AddWithValue(@"Id", poco.Id);
                    command.Parameters.AddWithValue(@"Login", poco.Login);
                    command.Parameters.AddWithValue(@"Current_Salary", poco.CurrentSalary);
                    command.Parameters.AddWithValue(@"Current_Rate", poco.CurrentRate);
                    command.Parameters.AddWithValue(@"Currency", poco.Currency);
                    command.Parameters.AddWithValue(@"Country_Code", poco.Currency);
                    command.Parameters.AddWithValue(@"State_Province_Code", poco.Province);
                    command.Parameters.AddWithValue(@"Street_Address", poco.Street);
                    command.Parameters.AddWithValue(@"City_Town", poco.City);
                    command.Parameters.AddWithValue(@"Zip_POstal_Code", poco.PostalCode);


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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[500];

            using (SqlConnection conn = new SqlConnection(connString))
            {

                SqlCommand command = new SqlCommand("SELECT * FROM Applicant_Profiles", conn);
                int position = 0;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.CurrentSalary = reader.GetDecimal(2);
                    poco.CurrentRate = reader.GetDecimal(3);
                    poco.Currency = reader.GetString(4);
                    poco.Country = reader.GetString(5);
                    poco.Province = reader.GetString(6);
                    poco.Street = reader.GetString(7);
                    poco.City = reader.GetString(8);
                    poco.PostalCode = reader.GetString(9);
                    poco.TimeStamp = (byte[])reader[10];

                    pocos[position] = poco;
                    position++;
                }
                pocos.ToList()
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            throw new NotImplementedException();
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            throw new NotImplementedException();
        }
    }
}
