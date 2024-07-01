CREATE OR ALTER PROCEDURE [dbo].[stp_GetTestSuits]
@ApplicationId				INT,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_GetTestSuits
CREATED BY		:	Mohammad Mobin
CREATED DATE	:	01 Jan 2024
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:
				EXEC stp_GetTestSuits
**************************************************************************************/
BEGIN TRY
	SELECT [TestSuites] = JSON_QUERY((
		SELECT DISTINCT [TestSuiteName], 'InBuilt' [TestSuiteFlag],
		[Application] = JSON_QUERY((
			SELECT [ApplicationName], [ApplicationId]
			FROM [dbo].[tbl_Applications] (NOLOCK) app
			WHERE app.[ApplicationId] = testCase.[ApplicationId]
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		))
		FROM [dbo].[tbl_TestCase] (NOLOCK) testCase
		WHERE (testCase.[RootId] IS NULL
			  OR testCase.[RootId] = 0)
			  AND testCase.[ApplicationId] = @ApplicationId
	FOR JSON PATH))
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() [TestSuites]
END CATCH