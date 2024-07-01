CREATE OR ALTER PROCEDURE [dbo].[stp_GetProjectRootRelation]
@ApplicationId				INT,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_GetProjectRootRelation
CREATED BY		:	Mohammed Yaseer
CREATED DATE	:	8th March 2024
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:
				EXEC stp_GetProjectRootRelation
**************************************************************************************/
BEGIN TRY
	SELECT [result] = JSON_QUERY((
		SELECT prr.[Id] AS [id],
               ISNULL(prr.[ParentId], '') AS [parentId],
               ISNULL(prr.[Name], '') AS [name]
		FROM [dbo].[tbl_ProjectRootRelation] (NOLOCK) prr
		ORDER BY prr.[Id] DESC
	FOR JSON PATH))
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() [ProjectRootRelation]
END CATCH