using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricalShop.Models
{
    public class InvoiceRepository:BaseRepository
    {
        public int AddInvoice(Invoice obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = obj.Id, DbType = DbType.AnsiString},
                new Parameter{ Name = "@MemberId", Value =obj.MemberId, DbType = DbType.AnsiString},
                new Parameter{ Name = "@Address", Value = obj.Address, DbType = DbType.AnsiString},
                new Parameter{ Name = "@PhoneNumber", Value = obj.PhoneNumber, DbType = DbType.AnsiString},
                new Parameter{ Name = "@PayMethod", Value = obj.PayMethod, DbType = DbType.Int32},
                new Parameter{ Name = "@MemberName", Value = obj.MemberName, DbType = DbType.AnsiString},
            };
            return Save("AddInvoice", parameters);
        }

        public List<Invoice> GetInvoceByMemberId(string id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetInvoceByMemberId";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter parameter = new Parameter
                    {
                        Name = "@MemberId",
                        Value = id,
                        DbType = DbType.AnsiString
                    };
                    SetParameter(command, parameter);
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Invoice> lst = new List<Invoice>();
                        while (reader.Read())
                        {
                            lst.Add( new Invoice
                            {
                                Id = (string)reader["Id"],
                                MemberName = (string)reader["MemberName"],
                                MemberId = (string)reader["MemberId"],
                                Address = (string)reader["Address"],
                                PhoneNumber=(string)reader["PhoneNumber"],
                                PayMethod = (int)reader["PayMethod"]
                            });
                        }
                        return lst;
                    }
                }
            }
        }

    }
}
