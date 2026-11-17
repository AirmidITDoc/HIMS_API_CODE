-- Use the shipped template and its existing mode-specific renderer for unit 1.
-- Preserve other units and customized templates.
SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF NOT EXISTS (
    SELECT 1 FROM dbo.M_ReportSetupOperational
    WHERE ReportId = 6 AND UnitId = 1 AND ReportMode = N'OPPaymentReceipt'
      AND ProcedureName = N'm_rptOPDPaymentReceiptPrint'
      AND ReportHtmlName IN (N'Samarth.html', N'OPPaymentReceipt.html')
)
BEGIN
    ROLLBACK TRANSACTION;
    THROW 50000, 'Unexpected OP payment receipt configuration. Review the mapping before applying this fix.', 1;
END;

UPDATE dbo.M_ReportSetupOperational
SET ReportHtmlName = N'OPPaymentReceipt.html',
    IsDBFunction = 0
WHERE ReportId = 6 AND UnitId = 1 AND ReportMode = N'OPPaymentReceipt'
  AND ProcedureName = N'm_rptOPDPaymentReceiptPrint'
  AND ReportHtmlName IN (N'Samarth.html', N'OPPaymentReceipt.html')
  AND (ReportHtmlName <> N'OPPaymentReceipt.html' OR IsDBFunction IS NULL OR IsDBFunction <> 0);

COMMIT TRANSACTION;
