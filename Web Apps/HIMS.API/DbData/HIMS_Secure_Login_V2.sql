/*
  Apply once before enabling /Login/Challenge or /Login/AuthenticateV2.
  Expired rows are purged opportunistically by the API; all consumes use
  DELETE ... OUTPUT so a challenge/confirmation can be accepted only once.
*/
IF OBJECT_ID(N'dbo.HimsLoginChallenge', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.HimsLoginChallenge
    (
        ChallengeId varchar(64) NOT NULL CONSTRAINT PK_HimsLoginChallenge PRIMARY KEY,
        Kid nvarchar(100) NOT NULL,
        ContextValue nvarchar(500) NOT NULL,
        Nonce varchar(100) NOT NULL,
        LoginType int NOT NULL,
        ExpiresAtUtc datetime2(7) NOT NULL,
        CaptchaHash varbinary(32) NOT NULL,
        CreatedAtUtc datetime2(7) NOT NULL
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_HimsLoginChallenge_ExpiresAtUtc'
               AND object_id = OBJECT_ID(N'dbo.HimsLoginChallenge'))
    CREATE INDEX IX_HimsLoginChallenge_ExpiresAtUtc ON dbo.HimsLoginChallenge (ExpiresAtUtc);

IF OBJECT_ID(N'dbo.HimsLoginConfirmation', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.HimsLoginConfirmation
    (
        ConfirmationId varchar(64) NOT NULL CONSTRAINT PK_HimsLoginConfirmation PRIMARY KEY,
        UserId bigint NOT NULL,
        LoginType int NOT NULL,
        ExpiresAtUtc datetime2(7) NOT NULL,
        CreatedAtUtc datetime2(7) NOT NULL
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_HimsLoginConfirmation_ExpiresAtUtc'
               AND object_id = OBJECT_ID(N'dbo.HimsLoginConfirmation'))
    CREATE INDEX IX_HimsLoginConfirmation_ExpiresAtUtc ON dbo.HimsLoginConfirmation (ExpiresAtUtc);
