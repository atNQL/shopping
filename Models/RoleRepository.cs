using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricalShop.Models
{
    public class RoleRepository:BaseRepository
    {
        public List<Role> GetRoles()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetRoles";
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    return FetchAll(command);
                }
            }
        }

        public int Add(Role obj)
        {
            Random random = new Random();
            Parameter[] parameters = {
                new Parameter{ Name = "@Name", Value = obj.Name, DbType = DbType.AnsiString},
                new Parameter{ Name = "@Id", Value = random.Next(), DbType = DbType.Int32}
            };
            return Save("AddRole", parameters);
        }
        static List<Role> FetchAll(IDbCommand command)
        {
            using (IDataReader reader = command.ExecuteReader())
            {
                List<Role> list = new List<Role>();
                while (reader.Read())
                {
                    list.Add(new Role
                    {
                        Id = (int)reader["RoleId"],
                        Name = (string)reader["RoleName"]
                    });
                }
                return list;
            }

        }
        public List<Role> GetRolesByMemberId(string id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetRolesByMemberId";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter parameter = new Parameter { Name = "@Id", Value = id, DbType = DbType.AnsiString };
                    SetParameter(command, parameter);
                    connection.Open();
                    return FetchAll(command);
                }
            }
        }
    }
}
