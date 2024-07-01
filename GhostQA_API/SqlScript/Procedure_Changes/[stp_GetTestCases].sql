CREATE OR ALTER PROCEDURE [dbo].[stp_GetTestCases]
@ApplicationId				INT = 1000,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_GetTestCases
CREATED BY		:	Mohammad Mobin
CREATED DATE	:	01 Jan 2023
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:
				EXEC stp_GetTestCases
**************************************************************************************/
BEGIN TRY
	SELECT [TestCasesListJson] = JSON_QUERY((
		SELECT DISTINCT t.[TestCaseName], 0 [IsSelected],
			   [Application] = JSON_QUERY((
					SELECT app.[ApplicationId], app.[ApplicationName]
					FROM [dbo].[tbl_Applications] (NOLOCK) app
					WHERE app.[ApplicationId] = t.[ApplicationId]
					FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
			   ))
		FROM [dbo].[tbl_TestCase] (NOLOCK) t
		WHERE t.[ApplicationId] = @ApplicationId
		FOR JSON PATH
	))
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() [TestCasesListJson]
END CATCH