using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleASPData.Models;
using Microsoft.Data.SqlClient;

namespace SampleASPData.Data
{
    public class SamuraiRepository : ISamurai
    {
        private readonly IConfiguration _configuration;
        public SamuraiRepository(IConfiguration configuration)
        {
            _configuration = configuration;    
        }

        public void Add(Samurai samurai)
        {
            using(SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var strSql = @"insert into Samurais (Name) values (@Name)";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Name", samurai.Name);
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if(result != 1)
                    {
                        throw new Exception("Error inserting data into the database!");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using(SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var strSql = @"DELETE FROM Samurais WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if(result != 1)
                    {
                        throw new Exception("Error deleting data into the database!");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public IEnumerable<Samurai> GetAll()
        {
            List<Samurai> samurais = new List<Samurai>();
            using(SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var strSql = @"SELECT Id, Name FROM Samurais
                                ORDER BY Name asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        samurais.Add(new Samurai
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return samurais;
        }

        public Samurai GetById(int id)
        {
             Samurai samurai = new Samurai();
             using(SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
             {
                var strSql = @"SELECT Id, Name FROM Samurais
                                WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        samurai.Id = Convert.ToInt32(dr["Id"]);
                        samurai.Name = dr["Name"].ToString();
                    }
                }

                dr.Close();
                cmd.Dispose();
                conn.Close();
             }
             return samurai;
        }

        public IEnumerable<Samurai> GetByName(string name)
        {
            List<Samurai> samurais = new List<Samurai>();
            using(SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var strSql = @"SELECT Id, Name FROM Samurais
                                WHERE Name LIKE @Name
                                ORDER BY Name asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Name", "%" + name + "%");
                
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        samurais.Add(new Samurai
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return samurais;
        }

        public void Update(int id,Samurai samurai)
        {
            using(SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var strSql = @"UPDATE Samurais SET Name = @Name
                                WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Name", samurai.Name);
                cmd.Parameters.AddWithValue("@Id", id);
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if(result != 1)
                    {
                        throw new Exception("Error updating data into the database!");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
    }
}