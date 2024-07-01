CREATE OR ALTER PROCEDURE [dbo].[stp_GetTestSuitsByName]
@TestSuiteName				VARCHAR(100),
@ApplicationId				INT = 1000,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_GetTestSuitsByName
CREATED BY		:	Mohammad Mobin
CREATED DATE	:	18 Jan 2024
MODIFIED BY		:	25th April 2024
MODIFIED DATE	:	Mohammad Mobin
PROC EXEC		:
				EXEC stp_GetTestSuitsByName
**************************************************************************************/
BEGIN TRY
	SELECT [result] = JSON_QUERY((
		SELECT
				[TestSuiteName],
				[TestSuiteType],
				JSON_QUERY((
					SELECT [ApplicationId], [ApplicationName]
					FROM tbl_Applications
					WHERE [ApplicationId] = t.[ApplicationId]
				FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
				)) [Application],
				[SendEmail],
				JSON_QUERY((
					SELECT [EnvironmentId], [EnvironmentName], [Baseurl], [BasePath], [DriverPath]
					FROM tbl_Environments
					WHERE EnvironmentId = t.[EnvironmentId]
				FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
				)) [Environment],
				JSON_QUERY((
					SELECT [TestUserId], [UserName], [PassWord]
					FROM tbl_TestUser
					WHERE Id = t.[TestUserId]
				FOR JSON PATH, WITHOUT_ARRAY_WRAPPER,INCLUDE_NULL_VALUES
				)) [TestUser],
				REPLACE([SelectedTestCases],' ','') [SelectedTestCases],
				[TestSuiteId],
				[Description]
		FROM [dbo].[tbl_TestSuites] (NOLOCK) t
		WHERE t.[TestSuiteName] = @TestSuiteName
			  AND t.[ApplicationId] = @ApplicationId
	FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
	))
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() [GetTestSuites]
END CATCH
