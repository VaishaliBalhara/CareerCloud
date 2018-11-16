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
    class ApplicantResumeRepository : BaseADO, IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach(ApplicantResumePoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]([Id],[Applicant],[Resume],[Last_Updated]
                    VALUES (@Id, @Applicant, @Resume, @Last_Updated)";
                    command.Parameters.AddWithValue(@"Id", poco.Id);
                    command.Parameters.AddWithValue(@"Applicant", poco.Applicant);
                    command.Parameters.AddWithValue(@"Resume", poco.Resume);
                    //command.Parameters.AddWithValue(@"Last_Update", poco.LastUpdated);

                    conn.Open();
                    int rowEdited = command.ExecuteNonQuery();
                    conn.Close();
                }
                
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {

            ApplicantResumePoco[] pocos = new ApplicantResumePoco[500];
            
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Applicant_Resumes", conn);
                int position = 0;

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Resume = reader.GetString(2);
                    poco.LastUpdated = reader.GetDateTime(3);

                    pocos[position] = poco;
                    position++;
                }
                return pocos.ToList();

            }

        }

        public IList<ApplicantResumePoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantResumePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantResumePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            throw new NotImplementedException();
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            throw new NotImplementedException();
        }
    }
}
