using System;
using Oetcker.Database;

namespace Oetcker.Libs.Interfaces
{
    public interface IDatabaseService
    {
        #region Methods

        DbConnection GetDbConnection();
        Action ConnectionChange { get; set; }

        #endregion
    }
}
