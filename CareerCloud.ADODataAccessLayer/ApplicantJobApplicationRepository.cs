﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
   public class ApplicantJobApplicationRepository : BaseADO, IDataRepository<ApplicantJobApplicationPoco>
    {
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach(ApplicantJobApplicationPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [JOB_PORTAL_DB].[dbo].[Applicant_Job_Applications]([Id],[Applicant],[Job],[Application_Date]) 
                    VALUES (@Id, @Applicant, @Job, @Application_Date)";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Job", poco.Job);
                    command.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);

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

        public IList<ApplicantJobApplicationPoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[500];
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("Select * from [JOB_PORTAL_DB].[dbo].[Applicant_Job_Applications]", conn);
                int position = 0;

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Job = reader.GetGuid(2);
                    poco.ApplicationDate = reader.GetDateTime(3);
                    poco.TimeStamp = (byte[])reader[4];

                    pocos[position] = poco;
                    position++;

                }
                conn.Close();
            }
            return pocos.Where(a => a!= null).ToList();
        }

        public IList<ApplicantJobApplicationPoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    command.CommandText = @"DELETE from [JOB_PORTAL_DB].[dbo].[Applicant_Job_Applications] where Id=@Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rowEffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    command.CommandText = @"UPDATE Applicant_Job_Applications SET Applicant=@Applicant,
                        Job=@Job, Application_Date=@ApplicationDate WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Job", poco.Job);
                    command.Parameters.AddWithValue("@ApplicationDate", poco.ApplicationDate);
                    command.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rowEffected = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
