-- Run against the API database before deploying the report-service guard.
-- Preserve every existing report customization; do not delete duplicate configuration rows.
DECLARE @Definition nvarchar(max) = OBJECT_DEFINITION(OBJECT_ID(N'dbo.ps_rptBillPrint'));
DECLARE @OldLookup nvarchar(200) = N't_LoginAccessDetails.AccessValueId=(SELECT M_LoginAccessConfig.LoginConfigId';
DECLARE @NewLookup nvarchar(200) = N't_LoginAccessDetails.AccessValueId IN (SELECT M_LoginAccessConfig.LoginConfigId';

IF @Definition IS NULL
    THROW 50000, 'Cannot read dbo.ps_rptBillPrint. Check the database and VIEW DEFINITION permission.', 1;

IF CHARINDEX(@NewLookup, @Definition) > 0
BEGIN
    PRINT 'The OP bill receipt stamp lookup is already corrected.';
    RETURN;
END;

DECLARE @LookupStart int = CHARINDEX(@OldLookup, @Definition);
IF @LookupStart = 0 OR CHARINDEX(@OldLookup, @Definition, @LookupStart + LEN(@OldLookup)) > 0
    THROW 50000, 'Unexpected OP bill receipt procedure definition. Review the stamp lookup manually.', 1;

SET @Definition = REPLACE(@Definition, @OldLookup, @NewLookup);

DECLARE @CreateStart int = CHARINDEX(N'CREATE PROCEDURE', @Definition);
IF @CreateStart = 0
    THROW 50000, 'Unexpected OP bill receipt procedure declaration. Review it manually.', 1;

SET @Definition = STUFF(@Definition, @CreateStart, LEN(N'CREATE'), N'ALTER');
EXEC sys.sp_executesql @Definition;
