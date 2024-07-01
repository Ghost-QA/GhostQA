CREATE OR ALTER PROCEDURE [dbo].[stp_GetPerformaceFile]
@RootId						INT,
@ApplicationId				INT,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_GetPerformaceFile
CREATED BY		:	Mohammed Yaseer
CREATED DATE	:	12th March 2024
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:
				EXEC stp_GetPerformaceFile
**************************************************************************************/
BEGIN TRY
	SELECT [result] = JSON_QUERY((
		SELECT pf.[Id] AS [id],
               ISNULL(pf.[RootId], '') [rootId],
               ISNULL(pf.[TestCaseName], '') [testCaseName],
			   ISNULL(pf.[FileName], '') [fileName],
			   ISNULL(pf.[FilePath], '') [FilePath],
			   [Application] = JSON_QUERY((
					SELECT app.[ApplicationId], app.[ApplicationName]
					FROM [dbo].[tbl_Applications] (NOLOCK) app
					WHERE app.[ApplicationId] = pf.[ApplicationId]
					FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
				))
		FROM [dbo].[tbl_PerformanceFile] (NOLOCK) pf
		WHERE [RootId] = @RootId
	FOR JSON PATH))
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() [PerformanceFile]
END CATCH