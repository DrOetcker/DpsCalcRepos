using System;
using Oetcker.Database;
using Oetcker.Libs.Interfaces;

namespace Oetcker.Libs.Services
{
    public class DatabaseService : IDatabaseService
    {
        #region Methods

        public DbConnection GetDbConnection()
        {
            var connection = DbConnection.Instance();
            connection.DatabaseName = "classicdb";
            if (connection.IsConnect())
                return connection;

            return null;
        }

        public Action ConnectionChange { get; set; }

        #endregion
    }
}
