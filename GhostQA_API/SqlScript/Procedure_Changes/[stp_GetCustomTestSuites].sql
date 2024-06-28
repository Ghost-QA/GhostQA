CREATE OR ALTER PROCEDURE [dbo].[stp_GetCustomTestSuites]
@ApplicationId				INT = 1000,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_GetCustomTestSuites
CREATED BY		:	Mohammad Mobin
CREATED DATE	:	15 Jan 2024
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:
				EXEC stp_GetCustomTestSuites
**************************************************************************************/
BEGIN TRY
	SELECT [testSuiteListJson] = JSON_QUERY((
		SELECT [TestSuiteId], [TestSuiteName],
			   [Application] = JSON_QUERY((
					SELECT app.[ApplicationId], app.[ApplicationName]
					FROM [dbo].[tbl_Applications] (NOLOCK) app
					WHERE app.[ApplicationId] = ts.[ApplicationId]
					FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
			   ))
		FROM [dbo].[tbl_TestSuites] (NOLOCK) ts
		WHERE ts.[ApplicationId] = @ApplicationId
		FOR JSON PATH
	))
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() [testSuiteListJson]
END CATCH