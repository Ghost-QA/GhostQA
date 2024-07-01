CREATE OR ALTER PROCEDURE [dbo].[stp_AddUpdateTestSuites]
@TestSuiteName				VARCHAR(100),
@TestSuiteType				VARCHAR(100),
@SendEmail					BIT,
@EnvironmentId				INT,
@SelectedTestCases			NVARCHAR(MAX),
@Description				NVARCHAR(MAX) = '',
@TestSuiteId				INT = 0,
@TestUserId					INT,
@RootId						INT,
@ApplicationId				INT = 1000,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_AddUpdateTestSuites
CREATED BY		:	Mohammad Mobin
CREATED DATE	:	15 Jan 2024
MODIFIED BY		:	Lokesh
MODIFIED DATE	:	12th June, 2024
PROC EXEC		:
				EXEC stp_AddUpdateTestSuites 'Mississippi', 0
**************************************************************************************/
BEGIN TRY
	IF EXISTS(SELECT TOP 1 1 FROM tbl_TestSuites (NOLOCK) WHERE [TestSuiteName] = @TestSuiteName AND [TestSuiteId] <> @TestSuiteId AND [RootId] = @RootId AND [ApplicationId] = @ApplicationId)
	BEGIN
		SELECT [result] = JSON_QUERY((
			SELECT 'fail' [status], 'Duplicate Custom Test Suite Name' [message]
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		))
	END
	ELSE IF @TestSuiteId = 0
	BEGIN
		IF NOT EXISTS(SELECT TOP 1 1 FROM tbl_TestSuites (NOLOCK) WHERE [TestSuiteName] = @TestSuiteName AND [TestSuiteId] <> @TestSuiteId AND [RootId] = @RootId AND [ApplicationId] = @ApplicationId)
		BEGIN
			INSERT INTO tbl_TestSuites ([TestSuiteName], [TestSuiteType], [SendEmail], [EnvironmentId], [SelectedTestCases], [Description], [TestUserId], [RootId], [ApplicationId], [OrganizationId], [TenantId]) 
			VALUES (@TestSuiteName, @TestSuiteType, @SendEmail, @EnvironmentId, @SelectedTestCases, @Description, @TestUserId, @RootId, @ApplicationId, @OrganizationId, @TenantId)

			INSERT INTO dbo.tbl_FunctionalSuiteRelation ([Name], [Parent], [ApplicationId], [OrganizationId], [TenantId])
			VALUES (@TestSuiteName, @RootId, @ApplicationId, @OrganizationId, @TenantId)
		END
		ELSE
		BEGIN
			SELECT [result] = JSON_QUERY((
			SELECT 'fail' [status], 'In-Built Test Suite already have this Name' [message]
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		))
		END
		IF @@ERROR = 0
		BEGIN
			SELECT [result] = JSON_QUERY((
				SELECT 'success' [status], 'Test Suite Saved Successfully' [message]
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
	END
	ELSE
	BEGIN
		DECLARE @OldTestSuiteName VARCHAR(200);
		
		SELECT @OldTestSuiteName = TestSuiteName 
		FROM tbl_TestSuites (NOLOCK)
		WHERE [TestSuiteId] = @TestSuiteId
			AND [RootId] = @RootId
			AND [ApplicationId] = @ApplicationId;

		UPDATE tbl_TestSuites
			SET [TestSuiteName]		= @TestSuiteName,
				[TestSuiteType]		= @TestSuiteType,
				[SendEmail]			= @SendEmail,
				[EnvironmentId]		= @EnvironmentId,
				[SelectedTestCases] = @SelectedTestCases,
				[Description]		= @Description,
				[TestUserId]        = @TestUserId,
				[ApplicationId]		= @ApplicationId,
				[OrganizationId]	= @OrganizationId,
				[TenantId]			= @TenantId
		WHERE [TestSuiteId] = @TestSuiteId
			  AND [RootId] = @RootId
			  AND [ApplicationId] = @ApplicationId

		UPDATE dbo.tbl_FunctionalSuiteRelation
		SET [Name] = @TestSuiteName
		WHERE Parent = @RootId
			  AND [ApplicationId] = @ApplicationId

		UPDATE tbl_TestCase
		SET TestSuiteName = @TestSuiteName
		WHERE TestSuiteName = @OldTestSuiteName
			  AND [ApplicationId] = @ApplicationId

		IF @@ERROR = 0
		BEGIN
			SELECT [result] = JSON_QUERY((
				SELECT 'success' [status], 'Test Suite Updated Successfully' [message]
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
	END
END TRY
BEGIN CATCH
	SELECT [result] = JSON_QUERY((
		SELECT 'fail' [status], ERROR_MESSAGE() [message]
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
	))
END CATCH