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
    class CompanyJobRepository : BaseADO, IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach(CompanyJobPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Company_Jobs] ([Id], [Company], [ProfileCreated], [IsInactive], [IsCompanyHidden]
                    VALUES (@Id, @Company, @ProfileCreated, @IsInactive, @IsCompanyHidden)";

                    command.Parameters.AddWithValue(@"Id", poco.Id);
                    command.Parameters.AddWithValue(@"Company", poco.Company);
                    command.Parameters.AddWithValue(@"ProfileCreated", poco.ProfileCreated);
                    command.Parameters.AddWithValue(@"IsInactive", poco.IsInactive);
                    command.Parameters.AddWithValue(@"IsCompanyHidden", poco.IsCompanyHidden);

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

        public IList<CompanyJobPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            CompanyJobPoco[] pocos = new CompanyJobPoco[500];

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("Select * from Company_Jobs", conn);
                int position = 0;

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.ProfileCreated = reader.GetDateTime(2);
                    poco.IsInactive = reader.GetBoolean(3);
                    poco.IsCompanyHidden = reader.GetBoolean(4);
                    poco.TimeStamp = (byte[])reader[5];

                    pocos[position] = poco;
                    position++;
                }
            }
            return pocos.ToList();
        }

        public IList<CompanyJobPoco> GetList(System.Linq.Expressions.Expression<Func<CompanyJobPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(System.Linq.Expressions.Expression<Func<CompanyJobPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            throw new NotImplementedException();
        }

        public void Update(params CompanyJobPoco[] items)
        {
            throw new NotImplementedException();
        }
    }
}
