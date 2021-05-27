using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ElectricalShop.Models
{
    public class MemberRepository : BaseRepository
    {
        public List<Member> GetMembers()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetMembers";
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Member> list = new List<Member>();
                        while (reader.Read())
                        {
                            list.Add(Fetch(reader));
                        }
                        return list;
                    }
                }
            }
        }

        static Member Fetch(IDataReader reader)
        {
            return new Member
            {
                Id = (string)reader["MemberId"],
                Username = (string)reader["Username"],
                Email = (string)reader["Email"],
                PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? (string)reader["PhoneNumber"] : "",
                Gender = (bool)reader["Gender"]
            };
        }

        static Member Fetch(IDbCommand command)
        {
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Fetch(reader);
                }
            }
            return null;
        }
        public Member SignIn(string usr, string pwd, out int ret)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SignIn";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter[] parameters =
                    {
                        new Parameter{ Name = "@Username", Value = usr, DbType = DbType.AnsiString},
                        new Parameter{ Name = "@Password", Value = Helper.Hash(usr + "@?@!*" + pwd), DbType = DbType.Binary},
                        new Parameter{ Name = "@Ret", DbType = DbType.Int32, Direction = ParameterDirection.ReturnValue }

                    };
                    SetParameter(command, parameters);
                    connection.Open();
                    Member member = Fetch(command);
                    IDbDataParameter parameter = (IDbDataParameter)command.Parameters["@Ret"];
                    ret = (int)parameter.Value;
                    return member;
                }
            }
        }

        public List<Role> GetMemberInRoles(string id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetMemberInRoles";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter parameter = new Parameter { Name = "@Id", Value = id, DbType = DbType.AnsiString };
                    SetParameter(command, parameter);
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Role> list = new List<Role>();
                        while (reader.Read())
                        {
                            list.Add(new Role
                            {
                                Id = (int)reader["RoleId"],
                                Name = (string)reader["RoleName"],
                                Checked = (bool)reader["Checked"]
                            });
                        }
                        return list;
                    }

                }
            }
        }

        public int Add(Member obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = Helper.RandomString(32), DbType = DbType.AnsiString},
                new Parameter{ Name = "@Username", Value =obj.Username, DbType = DbType.AnsiString},
                new Parameter{ Name = "@Password", Value = Helper.Hash(obj.Username + "@?@!*" + obj.Password), DbType = DbType.Binary},
                new Parameter{ Name = "@Email", Value = obj.Email, DbType = DbType.AnsiString},
                new Parameter{ Name = "@PhoneNumber", Value = obj.PhoneNumber, DbType = DbType.AnsiString},
                new Parameter{ Name = "@Gender", Value = obj.Gender, DbType = DbType.Boolean}
            };
            return Save("AddMember", parameters);
        }

        public int AddMembers(List<Member> list)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AddMember";
                    command.CommandType = CommandType.StoredProcedure;
                    IDbDataParameter parameterUsername = command.CreateParameter();
                    parameterUsername.ParameterName = "@Username";
                    parameterUsername.DbType = DbType.AnsiString;
                    command.Parameters.Add(parameterUsername);

                    IDbDataParameter parameterPassword = command.CreateParameter();
                    parameterPassword.ParameterName = "@Password";
                    parameterPassword.DbType = DbType.Binary;
                    command.Parameters.Add(parameterPassword);


                    IDbDataParameter parameterEmail = command.CreateParameter();
                    parameterEmail.ParameterName = "@Email";
                    parameterEmail.DbType = DbType.AnsiString;
                    command.Parameters.Add(parameterEmail);


                    IDbDataParameter parameterGender = command.CreateParameter();
                    parameterGender.ParameterName = "@Gender";
                    parameterGender.DbType = DbType.Boolean;
                    command.Parameters.Add(parameterGender);


                    IDbDataParameter parameterId = command.CreateParameter();
                    parameterId.ParameterName = "@Id";
                    parameterId.DbType = DbType.AnsiString;
                    command.Parameters.Add(parameterId);


                    connection.Open();
                    int ret = 0;
                    foreach (Member item in list)
                    {
                        parameterUsername.Value = item.Username;
                        parameterPassword.Value = Helper.Hash(item.Username + "@?@!*" + item.Password);
                        parameterEmail.Value = item.Email;
                        parameterGender.Value = item.Gender;
                        parameterId.Value = Helper.RandomString(32);
                        ret += command.ExecuteNonQuery();
                    }
                    return ret;
                }
            }
        }


        public int UpdateMemberInfor(Member obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = obj.Id, DbType = DbType.AnsiString},
                new Parameter{ Name = "@Username", Value =obj.Username, DbType = DbType.AnsiString},
                new Parameter{ Name = "@Email", Value = obj.Email, DbType = DbType.AnsiString},
                new Parameter{ Name = "@PhoneNumber", Value = obj.PhoneNumber, DbType = DbType.AnsiString},
                new Parameter{ Name = "@Gender", Value = obj.Gender, DbType = DbType.Boolean}
            };
            return Save("UpdateMemberInfor", parameters);
        }

        public Member GetMemberById(string id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetMemberById";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter parameter = new Parameter
                    {
                        Name = "@Id",
                        Value = id,
                        DbType = DbType.AnsiString
                    };
                    SetParameter(command, parameter);
                    connection.Open();
                    return Fetch(command);
                }
            }
        }

        public int DeleteMember(string id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DeleteMember";
                    command.CommandType = CommandType.StoredProcedure;
                    SetParameter(command, new Parameter { Name = "@Id", Value = id, DbType = DbType.AnsiString });
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }


    }
}
