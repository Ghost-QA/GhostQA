CREATE OR ALTER PROCEDURE [dbo].[stp_InsertBuiltInTestSuiteDetails]
@DynamicObject			NVARCHAR(MAX)
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_InsertBuiltInTestSuiteDetails
CREATED BY		:	Mohammad Mobin
CREATED DATE	:	15 Feb 2024
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:
				EXEC stp_InsertBuiltInTestSuiteDetails @DynamicObject
**************************************************************************************/
BEGIN TRY
	BEGIN
		DECLARE @ApplicationId				INT, 
				@ApplicationName			VARCHAR(100)

		SELECT @ApplicationName = [ApplicationName]
		FROM OPENJSON(@DynamicObject) WITH(
			[ApplicationName]     VARCHAR(100)
		)

		IF EXISTS(SELECT TOP 1 1 FROM [dbo].[tbl_Applications] (NOLOCK) WHERE [ApplicationName] = @ApplicationName)
		BEGIN
			SELECT @ApplicationId = [ApplicationId] FROM [dbo].[tbl_Applications] (NOLOCK) WHERE [ApplicationName] = @ApplicationName
		END
		ELSE
		BEGIN
			INSERT INTO tbl_Applications([ApplicationName])
			SELECT @ApplicationName

			SET @ApplicationId = SCOPE_IDENTITY()
		END


		INSERT INTO tbl_TestCase (
			[TestSuiteName], [TestRunName], [TestCaseName], [TestCaseStatus], [TestCaseVideoURL], [TestSuiteStartDateTime], 
			[TestSuiteEndDateTime], [TestRunStartDateTime], [TestRunEndDateTime], [TestCaseSteps], [TesterName], [TestEnvironment],
			[ApplicationId], [OrganizationId], [TenantId])
		SELECT
			[TestSuiteName], [TestRunName], [TestCaseName], [TestCaseStatus], [TestCaseVideoURL],
			CONVERT(DATETIMEOFFSET, [TestSuiteStartDateTime]), CONVERT(DATETIMEOFFSET, [TestSuiteEndDateTime]),
			CONVERT(DATETIMEOFFSET, [TestRunStartDateTime]), CONVERT(DATETIMEOFFSET, [TestRunEndDateTime]),
			[TestCaseSteps], [TesterName], [TestEnvironment], @ApplicationId, NULL, NULL
		FROM OPENJSON(@DynamicObject) WITH (
			[TestSuiteName]				NVARCHAR(100),
			[TestRunName]				NVARCHAR(100),
			[TestCaseName]				NVARCHAR(100),
			[TestCaseStatus]			NVARCHAR(50),
			[TestCaseVideoURL]			NVARCHAR(MAX),
			[TestSuiteStartDateTime]	DATETIMEOFFSET,
			[TestSuiteEndDateTime]		DATETIMEOFFSET,
			[TestRunStartDateTime]		DATETIMEOFFSET,
			[TestRunEndDateTime]		DATETIMEOFFSET,
			[TestCaseSteps]				NVARCHAR(MAX),
			[TesterName]				NVARCHAR(100),
			[TestEnvironment]			NVARCHAR(50))
	END

	IF @@ERROR = 0
	BEGIN
		SELECT [result] = JSON_QUERY((
			SELECT 'success' [status], 'Save Test Case Successfully' [message]
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