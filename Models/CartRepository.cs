using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricalShop.Models
{
    public class CartRepository:BaseRepository
    {
        public int UpdateQuantity(Cart obj)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UpdateQuantityCart";
                    command.CommandType = CommandType.StoredProcedure;
                    SetParameter(command, new Parameter[]
                    {
                        new Parameter{Name="@Id",Value =obj.Id,DbType=DbType.AnsiString},
                        new Parameter{Name="@ProductId",Value =obj.ProductId,DbType=DbType.Int32},
                        new Parameter{Name="@Quantity",Value =obj.Quantity,DbType=DbType.Int16}
                    });

                    connection.Open();
                    return command.ExecuteNonQuery();

                }
            }
        }
        public int DeleteProductInCart(string id, int productId)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DeleteProductInCart";
                    command.CommandType = CommandType.StoredProcedure;

                    SetParameter(command, new Parameter[]
                    {
                        new Parameter{Name="@Id",Value = id,DbType=DbType.AnsiString},
                        new Parameter{Name="@ProductId",Value = productId,DbType=DbType.Int32}
                    });

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int UpdateMemberId2Cart(string cardtId ,string memberId)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UpdateMemberId2Cart";
                    command.CommandType = CommandType.StoredProcedure;

                    SetParameter(command, new Parameter[]
                    {
                        new Parameter{Name="@CartId",Value = cardtId,DbType=DbType.AnsiString},
                        new Parameter{Name="@MemberId",Value = memberId,DbType=DbType.AnsiString}
                    });

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int Add(Cart obj)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AddCart";
                    command.CommandType = CommandType.StoredProcedure;

                    SetParameter(command, new Parameter[]
                    {
                        new Parameter{Name="@Id",Value =obj.Id,DbType=DbType.AnsiString},
                        new Parameter{Name="@ProductId",Value =obj.ProductId,DbType=DbType.Int32},
                        new Parameter{Name="@Quantity",Value =obj.Quantity,DbType=DbType.Int16},
                        new Parameter{Name="@MemberId",Value =obj.MemberId,DbType=DbType.AnsiString}
                    });

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
        public List<Cart> GetCarts(string id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetCarts";
                    command.CommandType = CommandType.StoredProcedure;
                    SetParameter(command, new Parameter { Name = "@Id", Value = id, DbType = DbType.AnsiString });
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Cart> list = new List<Cart>();
                        while (reader.Read())
                        {
                            Cart obj = new Cart
                            {
                                Id = (string)reader["CartId"],
                                ProductId = (int)reader["ProductId"],
                                Quantity = (short)reader["Quantity"],
                                ProductName = (string)reader["ProductName"],
                                ImageUrl = (string)reader["ImageUrl"],
                                Price = (int)reader["Price"]
                            };
                            list.Add(obj);
                        }
                        return list;
                    }
                }
            }
        }
        
        public List<Cart> GetCartsByMember(string id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetCartsByMember";
                    command.CommandType = CommandType.StoredProcedure;
                    SetParameter(command, new Parameter { Name = "@Id", Value = id, DbType = DbType.AnsiString });
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Cart> list = new List<Cart>();
                        while (reader.Read())
                        {
                            Cart obj = new Cart
                            {
                                Id = (string)reader["CartId"],
                                ProductId = (int)reader["ProductId"],
                                Quantity = (short)reader["Quantity"],
                                ProductName = (string)reader["ProductName"],
                                ImageUrl = (string)reader["ImageUrl"],
                                Price = (int)reader["Price"]
                            };
                            list.Add(obj);
                        }
                        return list;
                    }
                }
            }
        }

    }
}
