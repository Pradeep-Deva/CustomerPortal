﻿using Customer.Portal.Infrastructure.Contracts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Portal.DataAccess.Read.CustomerPortalRepo
{
    public class CustomerPortalBaseRepository
    {
        #region PRIVATE VARIABLES
        private IConnectionFactory _ConnectionFactory;
        #endregion

        #region CONSTRUCTOR
        public CustomerPortalBaseRepository(IConnectionFactory ConnectionFactory)
        {
            _ConnectionFactory = ConnectionFactory;
        }
        #endregion

        #region PUBLIC METHODS
        public async Task<int> ExecuteAsync(string sqlString, object parameters)
        {
            return await _ConnectionFactory.Connection.ExecuteAsync(sqlString, parameters);
        }
        public async Task<T> SelectFirstOrDefaultAsync<T>(string sqlString, object parameters)
        {
            return await _ConnectionFactory.Connection.QueryFirstOrDefaultAsync<T>(sqlString, parameters);
        }
        public async Task<IEnumerable<T>> SelectAsync<T>(string sqlString, object parameters)
        {
            return await _ConnectionFactory.Connection.QueryAsync<T>(sqlString, parameters);
        }
        #endregion
    }
}
