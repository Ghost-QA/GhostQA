CREATE OR ALTER PROCEDURE [dbo].[stp_AddLocation]
@PerformanceFileId			INT,
@Name						NVARCHAR(MAX),
@NumberUser					INT,
@PercentageTraffic			DECIMAL(18,2),
@ApplicationId				INT,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_AddLocation
CREATED BY		:	Mohammed Yaseer
CREATED DATE	:	13th March 2024
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:  EXEC stp_AddLocation 
				
**************************************************************************************/
BEGIN TRY
	BEGIN
		INSERT INTO [dbo].[tbl_PerformanceLocation] ([PerformanceFileId],[Name], [NumberUser], [PercentageTraffic], [ApplicationId], [OrganizationId], [TenantId]) 
		VALUES (@PerformanceFileId, @Name, @NumberUser, @PercentageTraffic, @ApplicationId, @OrganizationId, @TenantId)

		IF @@ERROR = 0
		BEGIN
			SELECT [Result] = JSON_QUERY((
				SELECT 'success' [status], 'Added Successfully' [message],
				[Data] = JSON_QUERY((
						SELECT pl.[Id] [id], pl.[PerformanceFileId] [performanceFileId], pl.[Name] [name],
							   pl.[NumberUser] [numberUser],pl.[PercentageTraffic] [percentageTraffic], 
							   [Application] = JSON_QUERY((
									SELECT app.[ApplicationId], app.[ApplicationName]
									FROM [dbo].[tbl_Applications] (NOLOCK) app
									WHERE app.[ApplicationId] = pl.[ApplicationId]
									FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
								))
						FROM [dbo].[tbl_PerformanceLocation] (NOLOCK) pl 
						where [Id] = SCOPE_IDENTITY()
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