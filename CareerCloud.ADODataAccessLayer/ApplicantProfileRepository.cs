
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
    public class ApplicantProfileRepository : BaseADO, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach(ApplicantProfilePoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [JOB_PORTAL_DB].[dbo].[Applicant_Profiles]([Id],[Login],[Current_Salary],[Current_Rate],[Currency],[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code])
                    VALUES (@Id, @Login, @Current_Salary, @Current_Rate, @Currency, @CountryCode, @Province, @Street, @City, @PostalCode)";
                    command.Parameters.AddWithValue(@"Id", poco.Id);
                    command.Parameters.AddWithValue(@"Login", poco.Login);
                    command.Parameters.AddWithValue(@"Current_Salary", poco.CurrentSalary);
                    command.Parameters.AddWithValue(@"Current_Rate", poco.CurrentRate);
                    command.Parameters.AddWithValue(@"Currency", poco.Currency);
                    command.Parameters.AddWithValue(@"CountryCode", poco.Country);
                    command.Parameters.AddWithValue(@"Province", poco.Province);
                    command.Parameters.AddWithValue(@"Street", poco.Street);
                    command.Parameters.AddWithValue(@"City", poco.City);
                    command.Parameters.AddWithValue(@"PostalCode", poco.PostalCode);


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

                conn.Open();
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
                return pocos.Where(a => a!= null).ToList();
                conn.Close();
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                conn.Open();
                command.CommandText = @"DELETE from Applicant_Profiles where Id=@Id";

                foreach (ApplicantProfilePoco poco in items)
                { 
                  
                    command.Parameters.AddWithValue("@Id", poco.Id);
                   
                    int rowEffected = command.ExecuteNonQuery();
                    
                }
                conn.Close();
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;

                    foreach (ApplicantProfilePoco poco in items)
                    {
                        command.CommandText = @"UPDATE [JOB_PORTAL_DB].[dbo].[Applicant_Profiles] SET Login=@Login,
                                            Current_Salary=@CurrentSalary,  Current_Rate=@CurrentRate,
                                                Currency=@Currency, Country_Code=@Country,
                                                State_Province_Code=@Province, Street_Address=@Street,
                                               City_Town=@City, Zip_Postal_Code=@PostalCode WHERE ID=@Id";
                        command.Parameters.AddWithValue("@Login", poco.Login);
                        command.Parameters.AddWithValue("@CurrentSalary", poco.CurrentSalary);
                        command.Parameters.AddWithValue("@CurrentRate", poco.CurrentRate);
                        command.Parameters.AddWithValue("@Currency", poco.Currency);
                        command.Parameters.AddWithValue("@Country", poco.Country);
                        command.Parameters.AddWithValue("@Province", poco.Province);
                        command.Parameters.AddWithValue("@Street", poco.Street);
                        command.Parameters.AddWithValue("@City", poco.City);
                        command.Parameters.AddWithValue("@PostalCode", poco.PostalCode);
                        command.Parameters.AddWithValue("@Id", poco.Id);

                        conn.Open();
                        int rowEffected = command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }
    }

