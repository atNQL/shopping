using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricalShop.Models
{
    public class PaymentRepository:BaseRepository
    {
        public List<Payment> GetPayments()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetPayments";
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Payment> payments = new List<Payment>();
                        while (reader.Read())
                        {
                            Payment obj = new Payment
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["PaymentMethod"]
                            };
                            payments.Add(obj);
                        }
                        return payments;
                    }
                }
            }


        }

        public int Add(Payment obj)
        {
            return Save("AddPayment", new Parameter { Name = "@Name", Value = obj.Name, DbType = DbType.AnsiString });
        }

        public int DeletePayment(int id)
        {
            return Save("DeletePayment", new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
        }
    }
}
