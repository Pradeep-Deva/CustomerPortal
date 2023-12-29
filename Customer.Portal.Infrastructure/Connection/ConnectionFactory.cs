using Customer.Portal.Infrastructure.ConfigurationManager;
using Customer.Portal.Infrastructure.Contracts;
using Microsoft.Extensions.Options; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.History.Infrastructure.Connection
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly ConnectionString _connectionString;
        private IDbConnection _dbConnection;
        public ConnectionFactory(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value;
        }
        public IDbConnection Connection
        {
            get
            {
                if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection = new SqlConnection(_connectionString.ReviewHistoryDBConnectionString);
                }
                return _dbConnection;
            }
        }
        public void Dispose()
        {
            if (_dbConnection != null)
            {
                _dbConnection.Dispose();
            }
        }
    }
}
