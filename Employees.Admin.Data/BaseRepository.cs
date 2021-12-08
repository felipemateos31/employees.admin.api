using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Admin.Data
{
    public class BaseRepository
    {
        private readonly IConfiguration _config;
        public BaseRepository(IConfiguration config)
        {
            this._config = config;
        }

        public SqlConnection GetOpenConnection()
        {
            string connectionString = _config["ConnectionStrings:Connectiondb"];
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
