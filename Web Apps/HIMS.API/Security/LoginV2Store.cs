using HIMS.Api.Models.Login;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;

namespace HIMS.API.Security
{
    public interface ILoginV2Store
    {
        Task CreateChallengeAsync(LoginV2Challenge challenge);
        Task<LoginV2Challenge?> ConsumeChallengeAsync(string id, DateTimeOffset now);
        Task<string> CreateConfirmationAsync(long userId, LoginType loginType, DateTimeOffset expiresAt);
        Task<(long UserId, LoginType LoginType)?> ConsumeConfirmationAsync(string token, LoginType loginType, DateTimeOffset now);
    }

    public sealed class DbLoginV2Store : ILoginV2Store
    {
        private readonly HIMSDbContext _db;
        public DbLoginV2Store(HIMSDbContext db) => _db = db;

        public async Task CreateChallengeAsync(LoginV2Challenge challenge)
        {
            var now = DateTime.UtcNow;
            await _db.Database.ExecuteSqlRawAsync(
                @"DELETE FROM dbo.HimsLoginChallenge WHERE ExpiresAtUtc < @p7;
                  INSERT INTO dbo.HimsLoginChallenge
                  (ChallengeId,Kid,ContextValue,Nonce,LoginType,ExpiresAtUtc,CaptchaHash,CreatedAtUtc)
                  VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)",
                challenge.Id, challenge.Kid, challenge.Context, challenge.Nonce, (int)challenge.LoginType,
                challenge.ExpiresAt.UtcDateTime, challenge.CaptchaHash, now);
        }

        public async Task<LoginV2Challenge?> ConsumeChallengeAsync(string id, DateTimeOffset now)
        {
            var connection = _db.Database.GetDbConnection();
            bool close = connection.State != ConnectionState.Open;
            if (close) await connection.OpenAsync();
            try
            {
                await using var command = connection.CreateCommand();
                command.CommandText =
                    @"DELETE FROM dbo.HimsLoginChallenge WITH (READPAST, ROWLOCK)
                      OUTPUT DELETED.Kid,DELETED.ContextValue,DELETED.Nonce,DELETED.LoginType,DELETED.ExpiresAtUtc,DELETED.CaptchaHash
                      WHERE ChallengeId=@id AND ExpiresAtUtc>=@now;";
                Add(command, "@id", id);
                Add(command, "@now", now.UtcDateTime);
                await using var reader = await command.ExecuteReaderAsync();
                if (!await reader.ReadAsync()) return null;
                return new LoginV2Challenge
                {
                    Id = id,
                    Kid = reader.GetString(0),
                    Context = reader.GetString(1),
                    Nonce = reader.GetString(2),
                    LoginType = (LoginType)reader.GetInt32(3),
                    ExpiresAt = new DateTimeOffset(DateTime.SpecifyKind(reader.GetDateTime(4), DateTimeKind.Utc)),
                    CaptchaHash = (byte[])reader[5]
                };
            }
            finally
            {
                if (close) await connection.CloseAsync();
            }
        }

        public async Task<string> CreateConfirmationAsync(long userId, LoginType loginType, DateTimeOffset expiresAt)
        {
            string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)).TrimEnd('=')
                .Replace('+', '-').Replace('/', '_');
            await _db.Database.ExecuteSqlRawAsync(
                @"DELETE FROM dbo.HimsLoginConfirmation WHERE ExpiresAtUtc < @p4;
                  INSERT INTO dbo.HimsLoginConfirmation
                  (ConfirmationId,UserId,LoginType,ExpiresAtUtc,CreatedAtUtc)
                  VALUES (@p0,@p1,@p2,@p3,@p4)",
                token, userId, (int)loginType, expiresAt.UtcDateTime, DateTime.UtcNow);
            return token;
        }

        public async Task<(long UserId, LoginType LoginType)?> ConsumeConfirmationAsync(
            string token, LoginType loginType, DateTimeOffset now)
        {
            var connection = _db.Database.GetDbConnection();
            bool close = connection.State != ConnectionState.Open;
            if (close) await connection.OpenAsync();
            try
            {
                await using var command = connection.CreateCommand();
                command.CommandText =
                    @"DELETE FROM dbo.HimsLoginConfirmation WITH (READPAST, ROWLOCK)
                      OUTPUT DELETED.UserId,DELETED.LoginType
                      WHERE ConfirmationId=@id AND LoginType=@type AND ExpiresAtUtc>=@now;";
                Add(command, "@id", token);
                Add(command, "@type", (int)loginType);
                Add(command, "@now", now.UtcDateTime);
                await using var reader = await command.ExecuteReaderAsync();
                return await reader.ReadAsync() ? (reader.GetInt64(0), (LoginType)reader.GetInt32(1)) : null;
            }
            finally
            {
                if (close) await connection.CloseAsync();
            }
        }

        private static void Add(IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}
