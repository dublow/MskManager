using System.Data;

namespace MskManager.Persistence.Providers
{
    public interface IProvider
    {
        IDbConnection Create();
    }
}
