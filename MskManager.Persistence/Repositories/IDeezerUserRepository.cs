using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MskManager.Persistence.Model;
using MskManager.Persistence.Providers;

namespace MskManager.Persistence.Repositories
{
    public interface IDeezerUserRepository
    {
        void Add(DeezerUser deezerUser);
        bool Exists(int id);
    }

    public class DeezerUserRepository : IDeezerUserRepository
    {
        private readonly IProvider _provider;

        public DeezerUserRepository(IProvider provider)
        {
            _provider = provider;
        }

        public void Add(DeezerUser deezerUser)
        {
            using (var query = _provider.Create())
            {
                query.Execute("dbo.AddDeezerUser", 
                    new {id = deezerUser.Id, accessToken = deezerUser.AccessToken},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public bool Exists(int id)
        {
            using (var query = _provider.Create())
            {
                var exists = query.QueryFirstOrDefault<bool>("dbo.DeezerUserExists",
                    new { id = id },
                    commandType: CommandType.StoredProcedure);

                return exists;
            }
        }
    }
}
