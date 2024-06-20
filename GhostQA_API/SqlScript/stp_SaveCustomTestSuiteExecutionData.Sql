CREATE OR ALTER PROCEDURE [dbo].[stp_SaveCustomTestSuiteExecutionData]
@TestSuiteJson			NVARCHAR(MAX)
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_SaveCustomTestSuiteExecutionData
CREATED BY		:	Mohammad Mobin
CREATED DATE	:	23 Jan 2024
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:  EXEC stp_SaveCustomTestSuiteExecutionData '{"TestSuiteName":"Custom Test Suite-3","TestRunName":"TestRun-1",
					"TestCaseName":"VerifyLoginOK","TestCaseStatus":"Passed","TestCaseVideoURL":"\\Recordings\\2024-01-23\\2024-01-23_07-14-32.webm",
					"TestSuiteStartDateTime":"2024-01-23T19:14:31.8909134+05:30","TestSuiteEndDateTime":"2024-01-23T19:14:54.4617051+05:30",
					"TestRunStartDateTime":"2024-01-23T19:14:49.04712+05:30","TestRunEndDateTime":"2024-01-23T19:14:54.1651478+05:30",
					"TestCaseSteps":"[{\"Status\":\"Passed\",\"Timestamp\":\"23-Jan-2024 19:14:49.0471200+05:30\",\"Details\":\"wait for plage to loader\",\"FailureMessage\":null,\"FailureException\":null,\"FailureScreenShots\":null},
					{\"Status\":\"Passed\",\"Timestamp\":\"23-Jan-2024 19:14:49.4064360+05:30\",\"Details\":\"Click on Login Button\",\"FailureMessage\":null,\"FailureException\":null,\"FailureScreenShots\":null},{\"Status\":\"Passed\",\"Timestamp\":\"23-Jan-2024 19:14:
52.6804401+05:30\",\"Details\":\"Enter Email Test\",\"FailureMessage\":null,\"FailureException\":null,\"FailureScreenShots\":null},{\"Status\":\"Passed\",\"Timestamp\":\"23-Jan-2024 19:14:53.4712594+05:30\",\"Details\":\"Enter passoword test\",\"FailureMe
ssage\":null,\"FailureException\":null,\"FailureScreenShots\":null},{\"Status\":\"Passed\",\"Timestamp\":\"23-Jan-2024 19:14:54.0119291+05:30\",\"Details\":\"Click on Submit button Test\",\"FailureMessage\":null,\"FailureException\":null,\"FailureScreenSh
ots\":null}]",
					"TesterName":"admin@gmail.com","TestEnvironment":"dev"}'
				
**************************************************************************************/
BEGIN TRY
	IF @TestSuiteJson <> '' OR @TestSuiteJson IS NOT NULL
	BEGIN
		INSERT INTO tbl_TestCase ([TestSuiteName], [TestRunName], [TestCaseName], [TestCaseStatus], [TestCaseVideoURL], 
								  [TestSuiteStartDateTime], [TestSuiteEndDateTime], [TestRunStartDateTime], [TestRunEndDateTime], 
								  [TestCaseSteps], [TesterName], [TestEnvironment], [RootId])
		SELECT
			TestSuiteName, TestRunName, TestCaseName, TestCaseStatus, TestCaseVideoURL,
			CONVERT(datetimeoffset, TestSuiteStartDateTime), CONVERT(datetimeoffset, TestSuiteEndDateTime),
			CONVERT(datetimeoffset, TestRunStartDateTime), CONVERT(datetimeoffset, TestRunEndDateTime),
			TestCaseSteps, TesterName, TestEnvironment, RootId
		FROM OPENJSON(@TestSuiteJson) WITH (
			TestSuiteName NVARCHAR(100),
			TestRunName NVARCHAR(100),
			TestCaseName NVARCHAR(100),
			TestCaseStatus NVARCHAR(50),
			TestCaseVideoURL NVARCHAR(MAX),
			TestSuiteStartDateTime DATETIMEOFFSET,
			TestSuiteEndDateTime DATETIMEOFFSET,
			TestRunStartDateTime DATETIMEOFFSET,
			TestRunEndDateTime DATETIMEOFFSET,
			TestCaseSteps NVARCHAR(MAX),
			TesterName NVARCHAR(100),
			TestEnvironment NVARCHAR(50),
			RootId INT)
	END
	ELSE
	BEGIN
		SELECT [result] = JSON_QUERY((
			SELECT 'fail' [status], 'Test Suite is Blank' [message]
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
END TRY
BEGIN CATCH
	SELECT [result] = JSON_QUERY((
			SELECT 'fail' [status], ERROR_MESSAGE() [message]
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		))
END CATCH