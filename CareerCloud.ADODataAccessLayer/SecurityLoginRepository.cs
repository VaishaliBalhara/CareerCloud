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
    class SecurityLoginRepository : BaseADO, IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach(SecurityLoginPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO [dbo].[Security_Logins] ([Id], [Login], [Password],[Created_Date],[Password_Update],[Agreement_Accepted_Date],[Is_Locked],[Is_Inactive],[Email_Address],[Phone_Number],[Full_Name],[Force_Change_Password],[Prefferred_Language]
                    VALUES (@Id,@Login,@Password,@Created,@PasswordUpdate,@AgreeementAccepted,@IsLocked,@IsInactive,@EmailAddress,@PhoneNumbber,@FullName,@ForceChangePassword,@PrefferredLAnguage)";

                    command.Parameters.AddWithValue(@"Id", poco.Id);
                    command.Parameters.AddWithValue(@"Login", poco.Login);
                    command.Parameters.AddWithValue(@"Password", poco.Password);
                    command.Parameters.AddWithValue(@"Created", poco.Created);
                    command.Parameters.AddWithValue(@"PasswordUpdate", poco.PasswordUpdate);
                    command.Parameters.AddWithValue(@"AgreementAccepted", poco.AgreementAccepted);
                    command.Parameters.AddWithValue(@"IsLocked", poco.IsLocked);
                    command.Parameters.AddWithValue(@"IsInactive", poco.IsInactive);
                    command.Parameters.AddWithValue(@"EmailAddress", poco.EmailAddress);
                    command.Parameters.AddWithValue(@"PhoneNumber", poco.PhoneNumber);
                    command.Parameters.AddWithValue(@"FullName", poco.FullName);
                    command.Parameters.AddWithValue(@"ForceChangePassword", poco.ForceChangePassword);
                    command.Parameters.AddWithValue(@"PrefferredLanguage", poco.PrefferredLanguage);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            SecurityLoginPoco[] pocos = new SecurityLoginPoco[500];

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("Select * from Security_Logins", conn);
                int position = 0;

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetString(1);
                    poco.Password = reader.GetString(2);
                    poco.Created = reader.GetDateTime(3);
                    poco.PasswordUpdate = reader.GetDateTime(4);
                    poco.AgreementAccepted = reader.GetDateTime(5);
                    poco.IsLocked = reader.GetBoolean(6);
                    poco.IsInactive = reader.GetBoolean(7);
                    poco.EmailAddress = reader.GetString(8);
                    poco.PhoneNumber = reader.GetString(9);
                    poco.FullName = reader.GetString(10);
                    poco.ForceChangePassword = reader.GetBoolean(11);
                    poco.PrefferredLanguage = reader.GetString(12);
                    poco.TimeStamp = (byte[])reader[13];

                    pocos[position] = poco;
                    position++;
                }
            }
            return pocos.ToList();
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            throw new NotImplementedException();
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            throw new NotImplementedException();
        }
    }
}
