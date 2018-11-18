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
    class ApplicantWorkHistoryRepository : BaseADO, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using(SqlConnection conn=new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach(ApplicantWorkHistoryPoco poco in itmes)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History] ([Id],[Applicant], [Comapny_Name], [Country_Code], [Location], [Job_Title], [Job_Description], [Start_Month], [Start_Year], [End_Month], [End_Year]
                    VALUES (@Id, @Applicant, @CompanyName, @COuntryCode, @Location, @JobTitle, @JobDescription, @StartMonth, @StartYear, @EndMonth, @EndYear)";

                    command.Parameters.AddWithValue(@"Id", poco.Id);
                    command.Parameters.AddWithValue(@"Applicant", poco.Applicant);
                    command.Parameters.AddWithValue(@"CompanyName", poco.CompanyName);
                    command.Parameters.AddWithValue(@"CountryCode", poco.CountryCode);
                    command.Parameters.AddWithValue(@"Location", poco.Location);
                    command.Parameters.AddWithValue(@"JobTitle", poco.JobTitle);
                    command.Parameters.AddWithValue(@"JobDescription", poco.JobDescription);
                    command.Parameters.AddWithValue(@"StartMonth", poco.StartMonth);
                    command.Parameters.AddWithValue(@"StartYear", poco.StartYear);
                    command.Parameters.AddWithValue(@"EndMonth", poco.EndMonth);
                    command.Parameters.AddWithValue(@"EndYear", poco.EndYear);

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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[500];

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("Select * from Applicant_Work_History", conn);
                int position = 0;

                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.CompanyName = reader.GetString(2);
                    poco.CountryCode = reader.GetString(3);
                    poco.Location = reader.GetString(4);
                    poco.JobTitle = reader.GetString(5);
                    poco.JobDescription = reader.GetString(6);
                    poco.StartMonth = reader.GetInt16(7);
                    poco.StartYear = reader.GetInt16(8);
                    poco.EndMonth = reader.GetInt16(9);
                    poco.EndYear = reader.GetInt16(10);
                    poco.TimeStamp = (byte[])reader[11];

                    pocos[position] = poco;
                    position++;
                }

            }
            return pocos.ToList();
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            throw new NotImplementedException();
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            throw new NotImplementedException();
        }
    }
}
