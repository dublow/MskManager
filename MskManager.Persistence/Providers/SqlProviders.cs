using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MskManager.Persistence.Providers
{
    public class SqlProviders : IProvider
    {
        private readonly string _cnx;

        public SqlProviders(ConnectionStringSettings cnx)
        {
            _cnx = cnx.ConnectionString;
        }

        public IDbConnection Create()
        {
            return new SqlConnection(_cnx);
        }
    }
}
