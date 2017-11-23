using CustomIdentityStores.Model;
using Dapper;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace CustomIdentityStores.Persistence
{
    public class UserTokenRepository
    {
        private IDbConnection _dbConnection;

        public UserTokenRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<int> Create(UserToken token, CancellationToken cancellationToken)
        {
            return _dbConnection.ExecuteAsync(@"
                INSERT INTO [dbo].[UserTokens]
                           ([Id]
                           ,[UserId]
                           ,[LoginProvider]
                           ,[Name]
                           ,[Value])
                     VALUES
                           (@Id
                           ,@UserId
                           ,@LoginProvider
                           ,@Name
                           ,@Value)", token);
        }

        public Task<int> Update(UserToken token, CancellationToken cancellationToken)
        {
            return _dbConnection.ExecuteAsync(@"
                UPDATE [dbo].[UserTokens]
                   SET [UserId] = @UserId
                      ,[LoginProvider] = @LoginProvider
                      ,[Name] = @Name
                      ,[Value] = @Value
                 WHERE [Id] = @Id", token);
        }

        public Task<UserToken> FindUserToken(
            string userId,
            string loginProvider,
            string tokenName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbConnection.QueryFirstOrDefaultAsync<UserToken>(@"
                SELECT [Id]
                      ,[UserId]
                      ,[LoginProvider]
                      ,[Name]
                      ,[Value]
                  FROM [dbo].[UserTokens]
                 WHERE [UserId] = @UserId
                   AND [LoginProvider] = @LoginProvider
                   AND [Name] = @Name",
                new { UserId = userId, LoginProvider = loginProvider, Name = tokenName });
        }
    }
}
