using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricalShop.Models
{
    public class InvoiceDetailRepository:BaseRepository
    {
        public int AddInvoiceDetail(InvoiceDetail obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@InvoiceId", Value = obj.InvoiceId, DbType = DbType.AnsiString},
                new Parameter{ Name = "@ProductId", Value =obj.ProductId, DbType = DbType.Int32},
                new Parameter{ Name = "@Price", Value = obj.Price, DbType = DbType.Int32},
                new Parameter{ Name = "@Quantity", Value = obj.Quantity, DbType = DbType.Int16},
            };
            return Save("AddInvoiceDetail", parameters);
        }

        public List<InvoiceDetail> GetInvoceDetailByInvoiceId(string id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetInvoceDetailByInvoiceId";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter parameter = new Parameter
                    {
                        Name = "@InvoiceId",
                        Value = id,
                        DbType = DbType.AnsiString
                    };
                    SetParameter(command, parameter);
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<InvoiceDetail> list = new List<InvoiceDetail>();
                        while (reader.Read())
                        {
                            list.Add(new InvoiceDetail
                            {
                                ProductId = (int)reader["ProductId"],
                                ProductName = (string)reader["ProductName"],
                                Price = (int)reader["Price"],
                                Quantity = (short)reader["Quantity"]
                            });
                        }
                        return list;
                    }
                }
            }
        }
    }
}
