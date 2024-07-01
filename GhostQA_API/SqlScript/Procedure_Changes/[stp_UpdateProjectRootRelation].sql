CREATE OR ALTER PROCEDURE [dbo].[stp_UpdateProjectRootRelation]
@Id							INT,
@Name						NVARCHAR(MAX),
@ApplicationId				INT,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_UpdateProjectRootRelation
CREATED BY		:	Mohammed Yaseer
CREATED DATE	:	8th March 2024
MODIFIED BY		:	Mohammed Yaseer
MODIFIED DATE	:	9th April 2024
PROC EXEC		:  EXEC stp_UpdateProjectRootRelation 
				
**************************************************************************************/
BEGIN TRY
IF EXISTS( SELECT TOP 1 1 FROM [dbo].[tbl_ProjectRootRelation] (NOLOCK) WHERE [Id] <> @Id AND [Name] = @Name AND [ApplicationId] = @ApplicationId)
BEGIN
	SELECT [result] = JSON_QUERY((
		SELECT 'fail' [status], 'Duplicate Work Space Name' [message]
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
	))
END
ELSE
	BEGIN
		UPDATE [dbo].[tbl_ProjectRootRelation]
		SET 
			[Name]				= @Name,
			[ApplicationId]		= @ApplicationId,
			[OrganizationId]	= @OrganizationId,
			[TenantId]			= @TenantId
		WHERE [Id]  = @Id
   END
		IF @@ERROR = 0
		BEGIN
			SELECT [result] = JSON_QUERY((
				SELECT 'success' [status], 'Updated Successfully' [message],
				[Data] = JSON_QUERY((
						SELECT prr.[Id] [id], prr.[ParentId] [parentId], prr.[Name] [name],
							   [Application] = JSON_QUERY((
									SELECT app.[ApplicationId], app.[ApplicationName]
									FROM [dbo].[tbl_Applications] (NOLOCK) app
									WHERE app.[ApplicationId] = prr.[ApplicationId]
									FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
							   ))
						FROM [dbo].[tbl_ProjectRootRelation] (NOLOCK) prr 
						WHERE prr.[Id]  = @Id
							  AND (@ApplicationId = 0 OR prr.[ApplicationId] = @ApplicationId)
						FOR JSON PATH
					))
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
	SELECT [result] = JSON_QUERY((
		SELECT 'fail' [status], ERROR_MESSAGE() [message]
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
	))
END CATCH