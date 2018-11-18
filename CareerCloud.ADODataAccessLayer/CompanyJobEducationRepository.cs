using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    class CompanyJobEducationRepository : BaseADO, IDataRepository<CompanyJobEducationPoco>
    {
        public void Add(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach(CompanyJobEducationPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Job_Educations] ([Id], [Job], [Major], [Importance]
                    VALUES (@Id, @Job, @Major, @Importance)";

                    command.Parameters.AddWithValue(@"Id", poco.Id);
                    command.Parameters.AddWithValue(@"Job", poco.Job);
                    command.Parameters.AddWithValue(@"Major", poco.Major);
                    command.Parameters.AddWithValue(@"Importance", poco.Importance);

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

        public IList<CompanyJobEducationPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            CompanyJobEducationPoco[] pocos = new CompanyJobEducationPoco[500];

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("Selecty * from Company_Job_Educations", conn);
                int position = 0;

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    CompanyJobEducationPoco poco = new CompanyJobEducationPoco();\
                    
                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.Major = reader.GetString(2);
                    poco.Importance = reader.GetInt16(3);
                    poco.TimeStamp = (byte[])reader[4];

                    pocos[position] = poco;
                    position++;
                    
                }
            }
            return pocos.ToList();
        }

        public IList<CompanyJobEducationPoco> GetList(System.Linq.Expressions.Expression<Func<CompanyJobEducationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(System.Linq.Expressions.Expression<Func<CompanyJobEducationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            throw new NotImplementedException();
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            throw new NotImplementedException();
        }
    }
}
