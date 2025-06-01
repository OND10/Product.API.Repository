using System.Data;

namespace ProductAPI.VSA.Common.Connection
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }

}
