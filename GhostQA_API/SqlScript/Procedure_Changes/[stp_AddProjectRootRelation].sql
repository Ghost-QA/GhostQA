CREATE OR ALTER PROCEDURE [dbo].[stp_AddProjectRootRelation]
@ParentId					INT,
@Name						NVARCHAR(MAX),
@ApplicationId				INT,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_AddProjectRootRelation
CREATED BY		:	Mohammed Yaseer
CREATED DATE	:	8th March 2024
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:  EXEC stp_AddProjectRootRelation 
				
**************************************************************************************/
BEGIN TRY
   IF EXISTS( SELECT TOP 1 1 FROM [dbo].[tbl_ProjectRootRelation] (NOLOCK) WHERE [ParentId] = @ParentId AND [Name] = @Name AND [ApplicationId] = @ApplicationId)
	BEGIN
		SELECT [result] = JSON_QUERY((
			SELECT 'fail' [status], 'Duplicate Work Space Name' [message]
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		))
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[tbl_ProjectRootRelation] ([ParentId], [Name], [ApplicationId], [OrganizationId], [TenantId]) 
		VALUES (@ParentId, @Name, @ApplicationId, @OrganizationId, @TenantId)
		IF @@ERROR = 0
		BEGIN
			SELECT [Result] = JSON_QUERY((
				SELECT 'success' [status], '' [message],
					[Data] = JSON_QUERY((
						SELECT prr.[Id] [id], prr.[ParentId] [parentId], prr.[Name] [name],
							   [Application] = JSON_QUERY((
									SELECT app.[ApplicationId], app.[ApplicationName]
									FROM [dbo].[tbl_Applications] (NOLOCK) app
									WHERE app.[ApplicationId] = prr.[ApplicationId]
									FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
							   ))
						FROM [dbo].[tbl_ProjectRootRelation] (NOLOCK) prr 
						WHERE prr.[Id] = SCOPE_IDENTITY()
							  AND (@ApplicationId = 0 OR prr.[ApplicationId] = @ApplicationId)
						FOR JSON PATH
					))
			FOR JSON PATH,WITHOUT_ARRAY_WRAPPER 
			))
		END
		ELSE
		BEGIN
			SELECT [result] = JSON_QUERY((
				SELECT 'fail' [status], CAST(@@ERROR AS NVARCHAR(20)) [message]
				FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
			))
		END
	END
END TRY
BEGIN CATCH
	SELECT [result] = JSON_QUERY((
		SELECT 'fail' [status], ERROR_MESSAGE() [message]
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
	))
END CATCH