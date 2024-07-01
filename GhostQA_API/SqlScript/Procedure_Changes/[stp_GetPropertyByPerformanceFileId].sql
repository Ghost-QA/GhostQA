CREATE OR ALTER PROCEDURE [dbo].[stp_GetPropertyByPerformanceFileId]
@PerformanceFileId          INT,
@ApplicationId				INT,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_GetPropertyByPerformanceFileId
CREATED BY		:	Mohammed Yaseer
CREATED DATE	:	14th March 2024
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:
				EXEC stp_GetPropertyByPerformanceFileId
**************************************************************************************/
BEGIN TRY
    IF EXISTS (SELECT TOP 1 1 FROM [dbo].[tbl_PerformanceProperties] (NOLOCK) WHERE [PerformanceFileId] = @PerformanceFileId AND [ApplicationId] = @ApplicationId)
    BEGIN
        SELECT [result] = JSON_QUERY((
            SELECT pp.[Id], 
                   ISNULL(pp.[PerformanceFileId], '')		[PerformanceFileId],
                   ISNULL(pp.[Name], '')					[Name],
				   ISNULL(pp.[Value], '')					[Value],
				   [Application] = JSON_QUERY((
						SELECT app.[ApplicationId], app.[ApplicationName]
						FROM [dbo].[tbl_Applications] (NOLOCK) app
						WHERE app.[ApplicationId] = pp.[ApplicationId]
						FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
					))
            FROM [dbo].[tbl_PerformanceProperties] (NOLOCK) pp
            WHERE PerformanceFileId = @PerformanceFileId
            FOR JSON PATH
        ))
    END
    ELSE
    BEGIN
        SELECT [result] = JSON_QUERY((
            SELECT 'fail' AS [status], 
                   'PerformanceFileId not found' AS [message]
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        ))
    END
END TRY
BEGIN CATCH
    SELECT [result] = JSON_QUERY((
        SELECT 'fail' AS [status], 
               ERROR_MESSAGE() AS [message]
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    ))
END CATCH