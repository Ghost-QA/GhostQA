CREATE OR ALTER PROCEDURE [dbo].[stp_GetFunctionalSuiteRelation]
@ApplicationId				INT = 1000,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_GetFunctionalSuiteRelation
CREATED BY		:	Lokesh
CREATED DATE	:	11th June, 2024	
PROC EXEC		:	EXEC stp_GetFunctionalSuiteRelation
**************************************************************************************/
BEGIN TRY
	SELECT [result] = JSON_QUERY((
		SELECT  [Id] [id], ISNULL([Parent], '') [parentId], ISNULL([Name], '') [name],
				[Application] = JSON_QUERY((
					SELECT app.[ApplicationId], app.[ApplicationName]
					FROM [dbo].[tbl_Applications] (NOLOCK) app
					WHERE app.[ApplicationId] = fsr.[ApplicationId]
					FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
				))
		FROM [dbo].[tbl_FunctionalSuiteRelation] (NOLOCK) fsr
		WHERE fsr.[ApplicationId] = @ApplicationId
	FOR JSON PATH))
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() [RootRelation]
END CATCH
