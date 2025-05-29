using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKMSDotNetTraining.Shared
{
    public class DapperService
    {

        private readonly string _connectionString;
        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, object? parameters = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString) ;
            var lst = db.Query<T>(query, parameters).ToList();
            return lst;
        }


        public T? QueryFirstOrDefault<T>(string query, object? parameters = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            T? item = db.QueryFirstOrDefault<T>(query, parameters) ;
            return item;
        }

        public int Execute(string query, object? parameters = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            int result = db.Execute(query, parameters);
             return result;

        }
        }
}
