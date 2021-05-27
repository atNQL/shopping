using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ElectricalShop.Models
{
    public class ProductRepository:BaseRepository
    {
        public int Count()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "CountProduct";
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }


        public Dictionary<int, List<Product>> GetProducts2()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetProducts";
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        Dictionary<int, List<Product>> dict = new Dictionary<int, List<Product>>();
                        while (reader.Read())
                        {
                            int key = (int)reader["CategoryId"];
                            if (!dict.ContainsKey(key))
                            {
                                dict.Add(key, new List<Product>());
                            }
                            Product obj = new Product
                            {
                                Id = (int)reader["ProductId"],
                                Name = (string)reader["ProductName"],
                                Price = (int)reader["Price"],
                                ImageUrl = (string)reader["ImageUrl"]
                            };
                            dict[key].Add(obj);
                        }
                        return dict;
                    }
                }
            }
        }

        public List<Product> SearchProducts(string q)
        {
            using (IDbConnection connection =  new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SearchProducts";
                    command.CommandType = CommandType.StoredProcedure;
                    IDbDataParameter parameter = command.CreateParameter();
                    parameter.Value = q;
                    parameter.ParameterName = "@Q";
                    parameter.DbType = DbType.String;
                    command.Parameters.Add(parameter);
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Product> list = new List<Product>();
                        while (reader.Read())
                        {
                            Product obj = new Product
                            {
                                Id = (int)reader["ProductId"],
                                Name = (string)reader["ProductName"],
                                Price = (int)reader["Price"],
                                Quantity = (short)reader["Quantity"],
                                ImageUrl = (string)reader["ImageUrl"],
                                Description = (string)reader["Description"],
                                CategoryId = (int)reader["CategoryId"]                                
                            };
                            list.Add(obj);
                        }
                        return list;
                    }

                }
            }
        }

        public List<Product> GetProductsRelation(int id, int categoryId)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetProductsRelation";
                    command.CommandType = CommandType.StoredProcedure;

                    Parameter[] parameters = { 
                        new Parameter{Name = "@Id", Value = id, DbType = DbType.Int32},
                        new Parameter{Name = "@CategoryId", Value = categoryId, DbType = DbType.Int32},
                    };

                    SetParameter(command, parameters);

                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Product> list = new List<Product>();
                        while (reader.Read())
                        {
                            Product obj = new Product
                            {
                                Id = (int)reader["ProductId"],
                                Name = (string)reader["ProductName"],
                                Description = (string)reader["Description"],
                                ImageUrl = (string)reader["ImageUrl"],
                                Price = (int)reader["Price"],
                                Quantity = (short)reader["Quantity"],
                                CategoryId = (int)reader["CategoryId"]
                            };
                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }
        }
        public Product GetProductById(int id)
        {
            //mo ket noi
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetProductById";
                    command.CommandType = CommandType.StoredProcedure;

                    SetParameter(command, new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
                    connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Fetch(reader);
                        }
                        return null;
                    }

                }
            }
        }

        public List<Product> GetProductsByCategory(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetProductsByCategory";
                    command.CommandType = CommandType.StoredProcedure;

                    SetParameter(command, new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Product> list = new List<Product>();
                        while (reader.Read())
                        {
                            Product obj = new Product
                            {
                                Id = (int)reader["ProductId"],
                                CategoryId = (int)reader["CategoryId"],
                                Name = (string)reader["ProductName"],
                                Price = (int)reader["Price"],
                                Quantity = (short)reader["Quantity"],
                                ImageUrl = (string)reader["ImageUrl"],
                                Description = (string)reader["Description"]
                            };
                            list.Add(obj);

                        }
                        return list;
                    }
                }
            }
        }

        public List<Product> GetProducts()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {

                    command.CommandText = "GetProducts";
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Product> list = new List<Product>();

                        while (reader.Read())
                        {
                            Product obj = Fetch(reader);
                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }
        }

        public List<Product> GetProductsPagination(int index, int size, out int total)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetProductsPagination";
                    Parameter[] parameters =
                    {
                        new Parameter{ Name = "@Start", Value = (index - 1) * size + 1, DbType = DbType.Int32},
                        new Parameter{ Name = "@End", Value = index * size, DbType = DbType.Int32},
                        new Parameter{ Name = "@Ret",Direction = ParameterDirection.ReturnValue, DbType = DbType.Int32}
                    };
                    SetParameter(command, parameters);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    List<Product> list = FetchAll(command);
                    IDataParameter parameter = (IDataParameter)command.Parameters["@Ret"];
                    total = (int)parameter.Value;

                    return list;
                }
            }
        }


        static Product Fetch(IDataReader reader)
        {
            return new Product
            {
                Description = (string)reader["Description"],
                Id = (int)reader["ProductId"],
                CategoryId = (int)reader["CategoryId"],
                Name = (string)reader["ProductName"],
                Price = (int)reader["Price"],
                Quantity = (short)reader["Quantity"],
                ImageUrl = (string)reader["ImageUrl"]
            };
        }

        static List<Product> FetchAll(IDbCommand command)
        {

            using (IDataReader reader = command.ExecuteReader())
            {
                List<Product> list = new List<Product>();
                while (reader.Read())
                {
                    list.Add(Fetch(reader));
                }
                return list;
            }
        }

        public int AddProduct(Product obj)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AddProduct";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter[] parameters =
                    {
                        new Parameter{Name = "@CategoryId", Value = obj.CategoryId,DbType=DbType.Int32},
                        new Parameter{Name = "@ProductName", Value = obj.Name,DbType=DbType.String},
                        new Parameter{Name = "@Price", Value = obj.Price,DbType=DbType.Int32},
                        new Parameter{Name = "@Quantity", Value = obj.Quantity,DbType=DbType.Int16},
                        new Parameter{Name = "@Description", Value = obj.Description,DbType=DbType.String},
                        new Parameter{Name = "@ImageUrl", Value = obj.ImageUrl,DbType=DbType.String}
                    };

                    SetParameter(command, parameters);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int Edit(Product obj)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "EditProduct";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter[] parameters =
                    {
                        new Parameter{Name = "@Id", Value = obj.Id,DbType=DbType.Int32},
                        new Parameter{Name = "@CategoryId", Value = obj.CategoryId,DbType=DbType.Int32},
                        new Parameter{Name = "@Name", Value = obj.Name,DbType=DbType.String},
                        new Parameter{Name = "@Price", Value = obj.Price,DbType=DbType.Int32},
                        new Parameter{Name = "@Quantity", Value = obj.Quantity,DbType=DbType.Int16},
                        new Parameter{Name = "@Description", Value = obj.Description,DbType=DbType.String},
                        new Parameter{Name = "@ImageUrl", Value = obj.ImageUrl,DbType=DbType.String}
                    };

                    SetParameter(command, parameters);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int Delete(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DeleteProduct";
                    command.CommandType = CommandType.StoredProcedure;
                    SetParameter(command, new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
    }

}