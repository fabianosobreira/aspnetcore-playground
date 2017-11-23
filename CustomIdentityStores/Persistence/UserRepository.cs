using CustomIdentityStores.Model;
using Dapper;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace CustomIdentityStores.Persistence
{
    public class UserRepository
    {
        private IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<int> Create(User applicationUser)
        {
            return _dbConnection.ExecuteAsync(@"
                INSERT INTO [dbo].[Users]
                           ([Id]
                           ,[UserName]
                           ,[Email]
                           ,[PhoneNumber]
                           ,[PasswordHash]
                           ,[TwoFactorEnabled]
                           ,[NormalizedUserName]
                           ,[NormalizedEmail]
                           ,[EmailConfirmed]
                           ,[PhoneNumberConfirmed])
                     VALUES
                           (@Id
                           ,@UserName
                           ,@Email
                           ,@PhoneNumber
                           ,@PasswordHash
                           ,@TwoFactorEnabled
                           ,@NormalizedUserName
                           ,@NormalizedEmail
                           ,@EmailConfirmed
                           ,@PhoneNumberConfirmed)", applicationUser);
        }

        public Task<int> Update(User applicationUser)
        {
            return _dbConnection.ExecuteAsync(@"
                UPDATE [dbo].[Users]
                   SET [UserName] = @UserName
                      ,[Email] = @Email
                      ,[PhoneNumber] = @PhoneNumber
                      ,[PasswordHash] = @PasswordHash
                      ,[TwoFactorEnabled] = @TwoFactorEnabled
                      ,[NormalizedUserName] = @NormalizedUserName
                      ,[NormalizedEmail] = @NormalizedEmail
                      ,[EmailConfirmed] = @EmailConfirmed
                      ,[PhoneNumberConfirmed] = @PhoneNumberConfirmed
                 WHERE [Id] = @Id", applicationUser);
        }

        internal Task<int> Delete(User applicationUser)
        {
            return _dbConnection.ExecuteAsync(@"
                DELETE FROM [dbo].[Users]
                 WHERE [Id] = @Id", applicationUser);
        }

        public Task<User> FindByName(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _dbConnection.QueryFirstOrDefaultAsync<User>(@"
                SELECT [Id]
                      ,[UserName]
                      ,[Email]
                      ,[PhoneNumber]
                      ,[PasswordHash]
                      ,[TwoFactorEnabled]
                      ,[NormalizedUserName]
                      ,[NormalizedEmail]
                      ,[EmailConfirmed]
                      ,[PhoneNumberConfirmed]
                  FROM [dbo].[Users]
                  WHERE [NormalizedUserName] = @NormalizedUserName", new { NormalizedUserName = normalizedUserName });
        }

        public Task<User> Find(string userId)
        {
            return _dbConnection.QueryFirstOrDefaultAsync<User>(@"
                SELECT [Id]
                      ,[UserName]
                      ,[Email]
                      ,[PhoneNumber]
                      ,[PasswordHash]
                      ,[TwoFactorEnabled]
                      ,[NormalizedUserName]
                      ,[NormalizedEmail]
                      ,[EmailConfirmed]
                      ,[PhoneNumberConfirmed]
                  FROM [dbo].[Users]
                  WHERE [Id] = @Id", new { Id = userId });
        }

        public Task<User> FindByEmail(string normalizedEmail, CancellationToken cancellationToken)
        {
            return _dbConnection.QueryFirstOrDefaultAsync<User>(@"
                SELECT [Id]
                      ,[UserName]
                      ,[Email]
                      ,[PhoneNumber]
                      ,[PasswordHash]
                      ,[TwoFactorEnabled]
                      ,[NormalizedUserName]
                      ,[NormalizedEmail]
                      ,[EmailConfirmed]
                      ,[PhoneNumberConfirmed]
                  FROM [dbo].[Users]
                  WHERE [NormalizedEmail] = @NormalizedEmail", new { Id = normalizedEmail });
        }
    }
}
