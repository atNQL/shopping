using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ElectricalShop.Models
{
    public abstract class BaseRepository
    {
        protected static string connectionString;
        static BaseRepository()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json").Build();
            connectionString = configuration.GetConnectionString("football");
        }

        protected static void SetParameter(IDbCommand command, Parameter parameter)
        {
            IDbDataParameter dataParameter = command.CreateParameter();
            dataParameter.ParameterName = parameter.Name;
            dataParameter.Value = parameter.Value;
            dataParameter.DbType = parameter.DbType;
            //chua co direction
            if (Enum.IsDefined(typeof(ParameterDirection), parameter.Direction))
            {
                dataParameter.Direction = parameter.Direction;
            }
            command.Parameters.Add(dataParameter);
        }
        protected static void SetParameter(IDbCommand command, Parameter[] parameters)
        {
            foreach (var parameter in parameters)
            {
                SetParameter(command, parameter);
            }
        }
        protected int Save(string sql, Parameter[] parameters)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.StoredProcedure;
                    SetParameter(command, parameters);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
        protected int Save(string sql, Parameter parameter)
        {
            return Save(sql, new Parameter[] { parameter });           
        }
    }
}
