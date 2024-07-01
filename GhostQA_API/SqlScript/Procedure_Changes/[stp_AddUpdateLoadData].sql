CREATE OR ALTER PROCEDURE [dbo].[stp_AddUpdateLoadData]
@PerformanceFileId			INT,
@TotalUser					INT,
@DurationMin				INT,
@RampupTime					INT,
@Steps						INT,
@ApplicationId				INT,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_AddLoad
CREATED BY		:	Mohammed Yaseer
CREATED DATE	:	15th March 2024
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:  EXEC stp_AddUpdateLoadData 
				
**************************************************************************************/
BEGIN TRY
  IF EXISTS( SELECT TOP 1 1 FROM [dbo].[tbl_Load] (NOLOCK) WHERE [PerformanceFileId] = @PerformanceFileId AND [ApplicationId] = @ApplicationId)
	BEGIN
		UPDATE [dbo].[tbl_Load]
			SET [TotalUsers] = @TotalUser,
				[DurationInMinutes] = @DurationMin,
				[RampUpTimeInSeconds] = @RampupTime,
				[RampUpSteps] = @Steps,
				[ApplicationId] = @ApplicationId
			WHERE [PerformanceFileId] = @PerformanceFileId
				  AND (@ApplicationId = 0 OR [ApplicationId] = @ApplicationId)
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[tbl_Load] ([PerformanceFileId], [TotalUsers], [DurationInMinutes], [RampUpTimeInSeconds], 
									  [RampUpSteps], [ApplicationId], [OrganizationId], [TenantId]) 
		SELECT @PerformanceFileId, @TotalUser, @DurationMin, @RampupTime, @Steps, @ApplicationId, @OrganizationId, @TenantId
	END
	
	IF @@ERROR = 0
	BEGIN
		SELECT [result] = JSON_QUERY((
			SELECT 'success' [status], 'Data Saved Successfully' [message]
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