CREATE OR ALTER PROCEDURE [dbo].[stp_GetDashBoardChartDetails]
@TestSuitName				VARCHAR(100),
@FilterType					VARCHAR(100),
@FilterValue				INT = 7,
@TimeZone					VARCHAR(100) = 'India Standard Time',
@RootId						INT = 0,
@ApplicationId				INT = 1000,
@OrganizationId				UNIQUEIDENTIFIER = NULL,
@TenantId					UNIQUEIDENTIFIER = NULL
AS
/**************************************************************************************
PROCEDURE NAME	:	stp_GetDashBoardChartDetails
CREATED BY		:	Mohammad Mobin
CREATED DATE	:	01 Jan 2023
MODIFIED BY		:	
MODIFIED DATE	:	
PROC EXEC		:
				EXEC [stp_GetDashBoardChartDetails] 'ClockSession', 'days' , 18
**************************************************************************************/
BEGIN TRY
    DECLARE @SQLQuery NVARCHAR(MAX)

    SET @SQLQuery = '
		WITH TestRuns AS (
			SELECT 
				CAST((CAST(t.[TestRunStartDateTime] AS DATETIMEOFFSET) AT TIME ZONE ''' + @TimeZone + ''') AS DATETIME) AS [TestRunStartDate],
				t.[TestSuiteName],
				t.[RootId],
				t.[TestRunName],
				t.[TestCaseStatus],
				t.[ApplicationId]
			FROM tbl_TestCase (NOLOCK) t
			WHERE t.[TestSuiteName] = ''' + @TestSuitName + '''
				  AND (''' + CAST(@RootId AS VARCHAR(100)) + ''' = ''0'' OR t.[RootId] = ''' + CAST(@RootId AS VARCHAR(100)) + ''')
				  AND (''' + CAST(@ApplicationId AS VARCHAR) + ''' = ''0'' OR t.[ApplicationId] = ''' + CAST(@ApplicationId AS VARCHAR) + ''')
		)

        SELECT [DashBoardDetailsJson] = JSON_QUERY((
            SELECT TOP ' + CAST(@FilterValue AS VARCHAR(10)) + ' 
				CAST(tr.[TestRunStartDate] AS DATE) [TestRunStartDate],
				tr.[TestRunName],
				CASE WHEN tr.TestRunName LIKE ''%TestRun%'' THEN ''Custom'' ELSE ''In-Built'' END AS [SuiteType],
				tr.[TestSuiteName],
				[Application] = JSON_QUERY((
					SELECT [ApplicationName], [ApplicationId]
					FROM [dbo].[tbl_Applications] (NOLOCK) app
					WHERE app.[ApplicationId] = tr.[ApplicationId]
					FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
				)),
				(
					SELECT COUNT(t1.[TestRunName])
					FROM tbl_TestCase (NOLOCK) t1
					WHERE t1.[TestSuiteName] = tr.[TestSuiteName]
							AND (''' + CAST(@RootId AS VARCHAR(100)) + ''' = ''0'' OR t1.[RootId] = tr.[RootId])
							AND (''' + CAST(@ApplicationId AS VARCHAR) + ''' = ''0'' OR t1.[ApplicationId] = ''' + CAST(@ApplicationId AS VARCHAR) + ''')
							AND t1.TestRunName = tr.TestRunName
							AND CAST((CAST(t1.[TestRunStartDateTime] AS DATETIMEOFFSET) AT TIME ZONE ''' + @TimeZone + ''') AS DATE) 
							= CAST(tr.[TestRunStartDate] AS DATE)
				) AS [TotalTestCase],
				(
					SELECT COUNT(t1.[TestRunName])
					FROM tbl_TestCase (NOLOCK) t1
					WHERE t1.[TestSuiteName] = tr.[TestSuiteName]
							AND (''' + CAST(@RootId AS VARCHAR(100)) + ''' = ''0'' OR t1.[RootId] = tr.[RootId])
							AND (''' + CAST(@ApplicationId AS VARCHAR) + ''' = ''0'' OR t1.[ApplicationId] = ''' + CAST(@ApplicationId AS VARCHAR) + ''')
							AND t1.TestRunName = tr.TestRunName
							AND CAST((CAST(t1.[TestRunStartDateTime] AS DATETIMEOFFSET) AT TIME ZONE ''' + @TimeZone + ''') AS DATE) 
							= CAST(tr.[TestRunStartDate] AS DATE)
							AND t1.[TestCaseStatus] LIKE ''%Passed%''
				) AS [TotalPassedTestCase],
				(
					SELECT COUNT(t1.[TestRunName])
					FROM tbl_TestCase (NOLOCK) t1
					WHERE t1.[TestSuiteName] = tr.[TestSuiteName]
							AND (''' + CAST(@RootId AS VARCHAR(100)) + ''' = ''0'' OR t1.[RootId] = tr.[RootId])
							AND (''' + CAST(@ApplicationId AS VARCHAR) + ''' = ''0'' OR t1.[ApplicationId] = ''' + CAST(@ApplicationId AS VARCHAR) + ''')
							AND t1.TestRunName = tr.TestRunName
							AND CAST((CAST(t1.[TestRunStartDateTime] AS DATETIMEOFFSET) AT TIME ZONE ''' + @TimeZone + ''') AS DATE) 
							= CAST(tr.[TestRunStartDate] AS DATE)
							AND t1.[TestCaseStatus] LIKE ''%Failed%''
				) AS [TotalFailedTestCase]
			FROM TestRuns tr
				GROUP BY
					tr.[ApplicationId],
					tr.[TestSuitename],
					tr.[TestRunName],
					CAST(tr.[TestRunStartDate] AS DATE),
					tr.[RootId]
				ORDER BY 
					CAST(tr.[TestRunStartDate] AS DATE) DESC, tr.[TestRunName] DESC
        FOR JSON PATH))'
	PRINT @SQLQuery

    IF @FilterType = 'runs'
        EXEC sp_executesql @SQLQuery
    ELSE
		WITH TestRuns AS (
			SELECT 
				CAST((CAST(t.[TestRunStartDateTime] AS DATETIMEOFFSET) AT TIME ZONE @TimeZone) AS DATETIME) AS [TestRunStartDate],
				t.[TestSuiteName],
				t.[RootId],
				t.[ApplicationId],
				t.[TestRunName],
				t.[TestCaseStatus]
			FROM tbl_TestCase (NOLOCK) t
			WHERE t.[TestSuiteName] = @TestSuitName
			AND (@RootId = 0 OR t.[RootId] = @RootId)
			AND (@ApplicationId = 0 OR t.[ApplicationId] = @ApplicationId)
		)

        SELECT [DashBoardDetailsJson] = JSON_QUERY((
            SELECT CAST(tr.[TestRunStartDate] AS DATE) [TestRunStartDate],
				tr.[TestRunName],
				CASE WHEN tr.TestRunName LIKE '%TestRun%' THEN 'Custom' ELSE 'In-Built' END AS [SuiteType],
				tr.[TestSuiteName],
				[Application] = JSON_QUERY((
					SELECT app.[ApplicationName], app.[ApplicationId]
					FROM [dbo].[tbl_Applications] (NOLOCK) app
					WHERE app.[ApplicationId] = tr.[ApplicationId]
					FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
				)),
				(
					SELECT COUNT(t1.[TestRunName])
					FROM tbl_TestCase (NOLOCK) t1
					WHERE t1.[TestSuiteName] = tr.[TestSuiteName]
						  AND (@RootId = 0 OR t1.[RootId] = tr.[RootId])
						  AND (@ApplicationId = 0 OR t1.[ApplicationId] = @ApplicationId)
						  AND CAST((CAST(t1.[TestRunStartDateTime] AS DATETIMEOFFSET) AT TIME ZONE @TimeZone) AS DATE) 
							= CAST(tr.[TestRunStartDate] AS DATE)
				) AS [TotalTestCase],
				(
					SELECT COUNT(t1.[TestRunName])
					FROM tbl_TestCase (NOLOCK) t1
					WHERE t1.[TestSuiteName] = tr.[TestSuiteName]
						  AND (@RootId = 0 OR t1.[RootId] = tr.[RootId])
						  AND (@ApplicationId = 0 OR t1.[ApplicationId] = @ApplicationId)
						  AND CAST((CAST(t1.[TestRunStartDateTime] AS DATETIMEOFFSET) AT TIME ZONE @TimeZone) AS DATE) 
							= CAST(tr.[TestRunStartDate] AS DATE)
						  AND t1.[TestCaseStatus] LIKE '%Passed%'
				) AS [TotalPassedTestCase],
				(
					SELECT COUNT(t1.[TestRunName])
					FROM tbl_TestCase (NOLOCK) t1
					WHERE t1.[TestSuiteName] = tr.[TestSuiteName]
						  AND (@RootId = 0 OR t1.[RootId] = tr.[RootId])
						  AND (@ApplicationId = 0 OR t1.[ApplicationId] = @ApplicationId)
						  AND CAST((CAST(t1.[TestRunStartDateTime] AS DATETIMEOFFSET) AT TIME ZONE @TimeZone) AS DATE) 
							= CAST(tr.[TestRunStartDate] AS DATE)
						  AND t1.[TestCaseStatus] LIKE '%Failed%'
				) AS [TotalFailedTestCase]
			FROM TestRuns tr
			WHERE CAST(tr.[TestRunStartDate] AS DATE) >= CAST(DATEADD(DAY, -@FilterValue, GETDATE() AT TIME ZONE @TimeZone) AS DATE)
			GROUP BY tr.[ApplicationId], tr.[TestSuiteName], tr.[TestRunName], CAST(tr.[TestRunStartDate] AS DATE), tr.[RootId]
			ORDER BY CAST(tr.[TestRunStartDate] AS DATE) DESC, tr.[TestRunName] DESC
        FOR JSON PATH))
END TRY
BEGIN CATCH
    SELECT [DashBoardDetailsJson] = JSON_QUERY((
		SELECT ERROR_MESSAGE() [ErrorMessage], ERROR_LINE() [ErrorLine]
	FOR JSON PATH))
END CATCH