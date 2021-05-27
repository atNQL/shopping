using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ElectricalShop.Models
{
    public class CategoryRepository : BaseRepository
    {
        //Lay chuoi ket noi tu file Web.config
        public int Delete(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DeleteCategory";
                    command.CommandType = CommandType.StoredProcedure;
                    SetParameter(command, new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
        public int Add(Category obj)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "AddCategory";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter[] parameters =
                    {
                        new Parameter { Name = "@Name", Value = obj.Name, DbType = DbType.String},
                        new Parameter { Name = "@ImageUrl", Value = obj.ImageUrl, DbType = DbType.String}
                    };
                    SetParameter(command, parameters);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int Edit(Category obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Id", Value = obj.Id, DbType = DbType.Int32},
                new Parameter{ Name = "@Name", Value = obj.Name, DbType = DbType.String},
                new Parameter{ Name = "@ImageUrl", Value = obj.ImageUrl, DbType = DbType.String}
            };
            return Save("EditCategory", parameters);
        }


        public Category GetCategoryById(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetCategoryById";
                    command.CommandType = CommandType.StoredProcedure;
                    SetParameter(command, new Parameter { Name = "@Id", Value = id, DbType = DbType.Int32 });

                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        //chi doc dong
                        if (reader.Read())
                        {
                            Category obj = new Category
                            {
                                Id = (int)reader["CategoryId"],
                                Name = (string)reader["CategoryName"],
                                ImageUrl= reader["CategoryImageUrl"] != DBNull.Value ? (string)reader["CategoryImageUrl"] : null
                            };
                            return obj;
                        }
                        return null;
                    }
                }
            }
        }
        public List<Category> GetCategories()
        {
            //Buoc tao ket noi
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //Buoc tao lenh thuc thu sql
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetCategories";
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Category> list = new List<Category>();
                        while (reader.Read())
                        {
                            Category obj = new Category
                            {
                                Id = (int)reader["CategoryId"],
                                Name = (string)reader["CategoryName"],
                                ImageUrl = reader["CategoryImageUrl"] != DBNull.Value ? (string)reader["CategoryImageUrl"] : null
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