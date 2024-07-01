CREATE OR ALTER PROCEDURE [dbo].[stp_DeleteTestSuites]
@TestSuiteName				VARCHAR(100),
@ApplicationId				INT = 1000,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_DeleteTestSuites
CREATED BY		:	Mohammad Mobin
CREATED DATE	:	15 Jan 2024
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:
				EXEC stp_DeleteTestSuites 1001
**************************************************************************************/
BEGIN TRY
	IF EXISTS(SELECT TOP 1 1 FROM [dbo].[tbl_TestSuites] (NOLOCK) WHERE [TestSuiteName] = @TestSuiteName AND [ApplicationId] = @ApplicationId)
	BEGIN
		IF EXISTS(SELECT TOP 1 1 FROM [dbo].[tbl_TestCase] (NOLOCK) WHERE [TestSuiteName] = @TestSuiteName AND [ApplicationId] = @ApplicationId)
		BEGIN
			DELETE FROM [dbo].[tbl_TestCase] WHERE [TestSuiteName] = @TestSuiteName AND [ApplicationId] = @ApplicationId
		END
		DELETE FROM [dbo].[tbl_TestSuites] WHERE [TestSuiteName] = @TestSuiteName AND [ApplicationId] = @ApplicationId
	END
	ELSE
	BEGIN
		SELECT [result] = JSON_QUERY((
			SELECT 'fail' [status], 'This is not a Custom Test Suite' [message]
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		))
	END

	IF @@ERROR = 0
	BEGIN
		SELECT [result] = JSON_QUERY((
			SELECT 'success' [status], 'Test Suite Deleted Successfully' [message]
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		))
	END
	ELSE
	BEGIN
		SELECT [result] = JSON_QUERY((
			SELECT 'fail' [status], CAST(@@ERROR AS NVARCHAR(20)) [message]
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		))
	END
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() [testSuiteListJson]
END CATCH