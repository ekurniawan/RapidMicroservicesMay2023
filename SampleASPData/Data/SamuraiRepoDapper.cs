using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleASPData.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace SampleASPData.Data
{
    public class SamuraiRepoDapper : ISamurai
    {
        private readonly IConfiguration _configuration;
        public SamuraiRepoDapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Add(Samurai samurai)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var strSql = @"insert into Samurais (Name) values (@Name)";
                var param = new { Name = samurai.Name };
                try
                {
                    conn.Open();
                    var result = conn.Execute(strSql, param);
                    if (result != 1)
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
                    conn.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var strSql = @"DELETE FROM Samurais WHERE Id = @Id";
                var param = new { Id = id };
                try
                {
                    conn.Open();
                    var result = conn.Execute(strSql, param);
                    if (result != 1)
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
                    conn.Close();
                }
            }
        }

        public IEnumerable<Samurai> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var strSql = @"SELECT Id, Name FROM Samurais
                                ORDER BY Name asc";
                var results = conn.Query<Samurai>(strSql);
                return results;
            }
        }

        public Samurai GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var strSql = @"SELECT Id, Name FROM Samurais
                                WHERE Id = @Id";
                var param = new { Id = id };
                var result = conn.QueryFirstOrDefault<Samurai>(strSql, param);
                if (result == null)
                {
                    throw new Exception($"Cannot find the samurai with id {id} in the database!");
                }
                return result;
            }
        }

        public IEnumerable<Samurai> GetByName(string name)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var strSql = @"SELECT Id, Name FROM Samurais
                                WHERE Name LIKE @Name
                                ORDER BY Name asc";
                var param = new { Name = $"%{name}%" };
                var results = conn.Query<Samurai>(strSql, param);
                return results;
            }
        }

        public void Update(int id, Samurai samurai)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var strSql = @"UPDATE Samurais SET Name = @Name
                                WHERE Id = @Id";
                var param = new { Id = id, Name = samurai.Name };
                try
                {
                    conn.Open();
                    var result = conn.Execute(strSql, param);
                    if (result != 1)
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
                    conn.Close();
                }
            }
        }
    }
}