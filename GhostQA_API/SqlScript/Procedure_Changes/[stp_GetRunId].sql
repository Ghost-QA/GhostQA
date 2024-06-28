CREATE OR ALTER PROCEDURE [dbo].[stp_GetRunId]
@TestSuite					VARCHAR(1000),
@ApplicationId				INT = 1000,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	[stp_GetRunId]
CREATED BY		:	Mohammad Mobin
CREATED DATE	:	18 Jan 2024
MODIFIED BY		:	25th April 2024
MODIFIED DATE	:	Mohammad Mobin
PROC EXEC		:
				EXEC [stp_GetRunId] 'ClockSession'
**************************************************************************************/
BEGIN TRY
	DECLARE @Count INT = 0
	IF OBJECT_ID('tbl_TestCase', 'U') IS NOT NULL
	BEGIN
		IF EXISTS(SELECT TOP 1 1 FROM [dbo].[tbl_TestCase] (NOLOCK))
		BEGIN
			SELECT @Count = ISNULL((MAX(CAST(REPLACE([TestRunName],'TestRun-','') AS INT))),0) 
			FROM [dbo].[tbl_TestCase] (NOLOCK)
			WHERE [TestSuiteName] = @TestSuite
				  AND [ApplicationId] = @ApplicationId
		END
	END
	SELECT CONCAT('TestRun-',CAST((@Count + 1) AS VARCHAR(20))) [TestRunName]
END TRY
BEGIN CATCH
	SELECT CONCAT('TestRun-',CAST(ERROR_MESSAGE() AS VARCHAR(20))) [TestRunName] 
END CATCH