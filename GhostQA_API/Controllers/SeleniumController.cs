using GhostQA_API.DTO_s;
using GhostQA_API.Helper;
using GhostQA_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace GhostQA_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class SeleniumController : ControllerBase
    {
        private readonly DBHelper _helper;

        public SeleniumController(DBHelper helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// Get Test Suites Name on Page Load of Report to showcase in Report
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetDataTestSuits")]
        public async Task<ActionResult> GetDataTestSuits()
        {
            return Ok(await _helper.GetDataTestSuits());
        }

        /// <summary>
        /// Get Test Run Over All Details by TestSuite Name
        /// </summary>
        /// <param name="testSuitName"></param>
        /// <param name="rootId"></param>
        /// <param name="applicationId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        /// <summary>
        /// Get Test Run Over All Details by TestSuite Name
        /// </summary>
        /// <param name="testSuitName"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetRunDetails")]
        public async Task<ActionResult> GetRunDetails(string testSuitName, int rootId)
        {
            if (!Request.Headers.TryGetValue("X-Api-Timezone", out StringValues timeZoneHeader))
            {
                return BadRequest("Timezone header is missing.");
            }

            string mapping = TimeZoneMappings.GetDBTimeZone(timeZoneHeader.ToString());
            return Ok(await _helper.GetRunDetails(testSuitName, rootId, mapping));
        }

        /// <summary>
        /// Get Test Case Details By TestSuite and Test Run Name
        /// </summary>
        /// <param name="testSuitName"></param>
        /// <param name="rootId"></param>
        /// <param name="runId"></param>
        /// <param name="applicationId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetTestCaseDetails")]
        public async Task<ActionResult> GetTestCaseDetails(string testSuitName, int rootId, string runId)
        {
            if (!Request.Headers.TryGetValue("X-Api-Timezone", out StringValues timeZoneHeader))
            {
                return BadRequest("Timezone header is missing.");
            }

            string mapping = TimeZoneMappings.GetDBTimeZone(timeZoneHeader.ToString());
            return Ok(await _helper.GetTestCaseDetails(testSuitName, rootId, runId, mapping));
        }

        /// <summary>
        /// Get Test Steps Details By TestSuite Name, Test Run Name and Test Case Name
        /// </summary>
        /// <param name="testSuitName"></param>
        /// <param name="rootId"></param>
        /// <param name="runId"></param>
        /// <param name="testCaseName"></param>
        /// <param name="applicationId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetTestCaseStepsDetails")]
        public async Task<ActionResult> GetTestCaseStepsDetails(string testSuitName, int rootId, string runId, string testCaseName)
        {
            if (!Request.Headers.TryGetValue("X-Api-Timezone", out StringValues timeZoneHeader))
            {
                return BadRequest("Timezone header is missing.");
            }

            string mapping = TimeZoneMappings.GetDBTimeZone(timeZoneHeader.ToString());
            return Ok(await _helper.GetTestCaseStepsDetails(testSuitName, rootId, runId, testCaseName, mapping));
        }

        /// <summary>
        /// Add or Update Test Suites on the basis of Test Suite Id
        /// </summary>
        /// <param name="TestSuiteObject"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddUpdateTestSuites")]
        public async Task<ActionResult> AddTestSuite(Dto_TestSuiteDetailsData model, string action)
        {
            Dto_Response _response = new Dto_Response();
            string result = await _helper.AddUpdateTestSuitesJson(model);
            _response = Newtonsoft.Json.JsonConvert.DeserializeObject<Dto_Response>(result);
            _response.Data = new { testSuiteName = model.TestSuiteName, actionType = action };
            return _response.status.Contains("fail") ? StatusCode(409, _response) : (ActionResult)Ok(_response);
        }

        /// <summary>
        /// Get Application Data
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetApplication")]
        public async Task<ActionResult> GetApplication()
        {
            return Ok(await _helper.GetApplications());
        }

        /// <summary>
        /// Get Environment Data
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetEnvironments")]
        public async Task<ActionResult> GetEnvironments()
        {
            return Ok(await _helper.GetEnvironments());
        }

        /// <summary>
        /// Get Test Suites in Json Format
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetTestSuites")]
        public async Task<ActionResult> GetTestSuites()
        {
            return Ok(await _helper.GetTestSuitesJson());
        }

        /// <summary>
        /// Get Test Cases in Json Format
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetTestCases")]
        public async Task<ActionResult> GetTestCases()
        {
            return Ok(await _helper.GetTestCasesJson());
        }

        /// <summary>
        /// Delete Test Suite By Test Suite Name
        /// </summary>
        /// <param name="TestSuiteId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("DeleteTestSuites")]
        public async Task<ActionResult> DeleteTestSuites(string TestSuiteName)
        {
            return Ok(await _helper.DeleteTestSuites(TestSuiteName));
        }

        /// <summary>
        /// Get Test Suite Details in Json Format by Name
        /// </summary>
        /// <param name="TestSuiteName"></param>
        /// <param name="applicationId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetTestSuiteByName")]
        public async Task<ActionResult> GetTestSuiteByName(string TestSuiteName, int RootId)
        {
            return Ok(await _helper.GetTestSuiteByName(TestSuiteName, RootId));
        }

        /// <summary>
        /// Execute a Test Suite by Test Suite Name
        /// </summary>
        /// <param name="TestSuiteName"></param>
        /// <returns></returns>
        [HttpPost("ExecuteTestSuite")]
        public async Task<ActionResult> ExecuteTestSuite(Dto_Execution model)
        {
            if (await _helper.IsAnySuiteRunning())
                return Ok(new { status = "Conflict", message = "Another test is already running on GhostQA. Please try after some time." });

            await _helper.UpdateSuiteRunStatus(true);

            if (!Request.Headers.TryGetValue("X-Api-Timezone", out StringValues timeZoneHeader))
            {
                await _helper.UpdateSuiteRunStatus(false);
                return BadRequest("Timezone header is missing.");
            }

            string mapping = TimeZoneMappings.GetDBTimeZone(timeZoneHeader.ToString());

            List<object> _result = new List<object>();
            string _testRunName = string.Empty;
            string _testSuiteDetailsJson = string.Empty;
            TestSuiteNameData _testSuiteNameData = null;
            string testerName = model.userEmail;
            string result = string.Empty;
            string projectName = string.Empty;
            Models.Environments _environmentDetails = null;

            try
            {
                _testRunName = await _helper.GetRunId(model.testSuiteName);
                _testSuiteDetailsJson = await _helper.GetTestSuiteByName(model.testSuiteName, model.rootId);
                _testSuiteNameData = Newtonsoft.Json.JsonConvert.DeserializeObject<TestSuiteNameData>(_testSuiteDetailsJson);
                projectName = _testSuiteNameData.Application.ApplicationName;
                _environmentDetails = await _helper.GetEnvironmentById(Convert.ToInt32(_testSuiteNameData.Environment.EnvironmentId));
            }
            catch (Exception ex)
            {
                _result.Add(new { status = "Failed", message = ex.Message });
                await _helper.UpdateSuiteRunStatus(false);
                return Ok(_result);
            }

            if (_testSuiteNameData.SelectedTestCases != null && _testSuiteNameData.SelectedTestCases.Length > 0)
            {
                int totalTestCases = _testSuiteNameData.SelectedTestCases.Split(",").Length;
                int counter = 0;
                foreach (var testCaseName in _testSuiteNameData.SelectedTestCases.Split(","))
                {
                    string _testCaseJsonData = await _helper.RunTestCase(projectName, model.testSuiteName, testCaseName.ToString(), _testRunName, testerName, _testSuiteNameData.Environment.BaseUrl, _testSuiteNameData.Environment.BasePath, _testSuiteNameData.Environment.EnvironmentName, _environmentDetails.BrowserName, _testSuiteNameData.Environment.DriverPath, _testSuiteNameData.TestUser.UserName, _testSuiteNameData.TestUser.Password);

                    if (string.IsNullOrEmpty(_testCaseJsonData))
                    {
                        counter++;
                    }
                    else
                    {
                        Dto_TestCaseData _testSuiteData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dto_TestCaseData>(_testCaseJsonData);
                        _testSuiteData.TestSuiteName = model.testSuiteName;
                        _testSuiteData.TesterName = testerName;
                        _testSuiteData.TestRunName = _testRunName;
                        _testSuiteData.TestEnvironment = _environmentDetails.EnvironmentName;
                        _testSuiteData.TestBrowserName = _environmentDetails?.BrowserName;
                        _testSuiteData.TestCaseName = testCaseName;
                        _testSuiteData.RootId = model.rootId;
                        _testSuiteData.ApplicationId = model.ApplicationId;
                        _testSuiteData.OrganizationId = model.OrganizationId;
                        _testSuiteData.TenantId = model.TenantId;
                        result = await _helper.SaveTestCaseData(Newtonsoft.Json.JsonConvert.SerializeObject(_testSuiteData));
                        _result.Add(result);
                    }
                }

                if (counter == totalTestCases)
                {
                    await _helper.UpdateSuiteRunStatus(false);
                    _result.Add(new { status = "Failed", message = "TestCases failed to execute" });
                    return Ok(_result);
                }

                try
                {
                    if (result.Contains("success"))
                    {
                        string originalUrl = Request.Headers.Referer.ToString();
                        int lastSlashIndex = originalUrl.LastIndexOf('/');
                        var Url = lastSlashIndex != -1 ? originalUrl.Substring(0, lastSlashIndex + 1) : originalUrl;
                        if (_testSuiteNameData.SendEmail == true)
                        {
                            var obj = await _helper.SendExecutionDataMail(model.testSuiteName, _testRunName, testerName, Url, mapping);
                            _result.Add(obj);
                        }
                        else
                        {
                            string AllUsers = await _helper.GetUserDetails();
                            JArray jsonArray = JArray.Parse(AllUsers);
                            List<string> emails = jsonArray.Where(j => !(bool)j["IsDisabled"]).Select(j => (string)j["Email"]).ToList();
                            string commaSeparatedEmails = string.Join(", ", emails);

                            var obj1 = await _helper.SendExecutionDataMail(model.testSuiteName, _testRunName, commaSeparatedEmails, Url, mapping);
                            _result.Add(obj1);
                        }

                        var teamDetail = await _helper.GetAllUserIntegration(model.userId);
                        var webhookUrl = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dto_IntegrationRespnse>>(teamDetail);

                        if (webhookUrl[1].IsIntegrated)
                        {
                            await _helper.PostReportInTeams(model.testSuiteName, _testRunName, testerName, _environmentDetails.EnvironmentName, webhookUrl[1].APIKey, Url, mapping);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _result.Add(new { status = "Failed", message = ex.Message });
                    await _helper.UpdateSuiteRunStatus(false);
                    return Ok(_result);
                }
            }

            await _helper.UpdateSuiteRunStatus(false);
            _result.Add(new { status = "Finished", message = "Test Suite execution completed!" });
            return Ok(_result);
        }

        /// <summary>
        /// Add / Update Environments
        /// </summary>
        /// <param Environments=Environments></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddUpdateEnvironment")]
        public async Task<ActionResult> AddUpdateEnvironment([FromBody] Models.Environments model)
        {
            try
            {
                var result = await _helper.AddUpdateEnvironmentJson(model);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Add / Update Environments
        /// </summary>
        /// <param Applications=Applications></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddUpdateApplication")]
        public async Task<ActionResult> AddUpdateApplication([FromBody] Models.Applications model)
        {
            try
            {
                var result = await _helper.AddUpdateApplicationJson(model);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Add / Update Environments
        /// </summary>
        /// <param Browser=Browser></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddUpdateBrowser")]
        public async Task<ActionResult> AddUpdateBrowser([FromBody] Models.Browsers model)
        {
            try
            {
                var result = await _helper.AddUpdateBrowserJson(model);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get Browser in Json Format
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetBrowsers")]
        public async Task<ActionResult> GetBrowsers()
        {
            return Ok(await _helper.GetBrowsers());
        }

        /// <summary>
        /// Get Environment in Json Format by Id
        /// </summary>
        /// <param Id="Id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetEnvironmentById")]
        public async Task<ActionResult> GetEnvironmentById(int Id)
        {
            return Ok(await _helper.GetEnvironmentById(Id));
        }

        /// <summary>
        /// Execute Test Case
        /// </summary>
        /// <param TestCaseData=TestCaseData></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("SaveTestCaseData")]
        public async Task<ActionResult> SaveTestCaseData(string testSuiteJsonData)
        {
            try
            {
                var result = await _helper.SaveTestCaseData(testSuiteJsonData);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Get Test Suite Name
        /// </summary>
        /// <param Name="Name"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetRunId")]
        public async Task<ActionResult> GetRunId(string testSuiteName)
        {
            return Ok(await _helper.GetRunId(testSuiteName));
        }

        /// <summary>
        /// Get Chart Details for Dashboard Local Testing
        /// </summary>
        /// <param name="TestSuiteName"></param>
        /// <param name="RootId"></param>
        /// <param name="Filtertype"></param>
        /// <param name="FilterValue"></param>
        /// <param name="applicationId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetChartDetails")]
        public async Task<ActionResult> GetDashboardChartDetails(string TestSuiteName, int RootId, string Filtertype, int FilterValue)
        {
            if (!Request.Headers.TryGetValue("X-Api-Timezone", out StringValues timeZoneHeader))
            {
                return BadRequest("Timezone header is missing.");
            }

            string mapping = TimeZoneMappings.GetDBTimeZone(timeZoneHeader.ToString());

            try
            {
                string result = await _helper.GetDashboardChartDetails(TestSuiteName, RootId, Filtertype, FilterValue, mapping);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Delete Application By ApplicationId
        /// </summary>
        /// <param name="ApplicationId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("DeleteApplication")]
        public async Task<ActionResult> DeleteApplication()
        {
            return Ok(await _helper.DeleteApplication());
        }

        /// <summary>
        /// Delete Browser By BrowserId
        /// </summary>
        /// <param Int="BrowserId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("DeleteBrowser")]
        public async Task<ActionResult> DeleteBrowser(int BrowserId)
        {
            return Ok(await _helper.DeleteBrowser(BrowserId));
        }

        /// <summary>
        /// Delete Environment By EnvironmentId
        /// </summary>
        /// <param Int="EnvironmentId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("DeleteEnvironment")]
        public async Task<ActionResult> DeleteEnvironment(int EnvironmentId)
        {
            return Ok(await _helper.DeleteEnvironment(EnvironmentId));
        }

        /// <summary>
        /// Check Test Execution is in progress or not
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("IsExecutionInProgress")]
        public async Task<ActionResult> IsExecutionInProgress()
        {
            return Ok(await _helper.GetExecutionInProgress());
        }

        /// <summary>
        /// Get User in Json Format
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetUserDetails")]
        public async Task<ActionResult> GetUserDetails()
        {
            return Ok(await _helper.GetUserDetails());
        }

        /// <summary>
        /// Update User Profile
        /// </summary>
        /// <param updatedUserProfile="updatedUserProfile"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("UpdateUserProfile")]
        public async Task<ActionResult> UpdateUserProfile(Dto_UpdateUserProfile model)
        {
            return Ok(await _helper.UpdateUserProfile(model));
        }

        /// <summary>
        /// Get User Profile
        /// </summary>
        /// <param Email="Email"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("GetProfilByEmail")]
        public async Task<ActionResult> GetProfilByEmail(string Email)
        {
            return Ok(await _helper.GetProfilByEmail(Email));
        }

        /// <summary>
        /// Disable Enable User
        /// </summary>
        /// <param DisableEnableUser="Dto_DisableEnableUser"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("DisableEnableUser")]
        public async Task<ActionResult> DisableEnableUser(Dto_DisableEnableUser model)
        {
            return Ok(await _helper.DisableEnableUser(model));
        }

        /// <summary>
        /// Get All Test User List
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetAllTestUser")]
        public async Task<ActionResult> GetAllTestUser()
        {
            return Ok(await _helper.GetAllTestUser());
        }

        /// <summary>
        /// Get Test User  By Id
        /// </summary>
        /// <param Id = Id></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetTestUserById")]
        public async Task<ActionResult> GetTestUserById(int Id)
        {
            return Ok(await _helper.GetTestUserById(Id));
        }

        /// <summary>
        /// Add Test User
        /// </summary>
        /// <param TestUser = TestUser></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddTestUser")]
        public async Task<ActionResult> AddTestUser(TestUser model)
        {
            var CreatedBy = User.FindFirst(ClaimTypes.Email)?.Value.ToString();
            return Ok(await _helper.AddTestUser(model, CreatedBy));
        }

        /// <summary>
        /// Delete Test User By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("DeleteTestUser")]
        public async Task<ActionResult> DeleteTestUser(int Id)
        {
            return Ok(await _helper.DeleteTestUser(Id));
        }

        /// <summary>
        /// Add Test User
        /// </summary>
        /// <param Dto_UserOrganization = Dto_UserOrganization></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddUpdateUserOrganization")]
        public async Task<ActionResult> AddUpdateUserOrganization([FromForm] Dto_UserOrganization model)
        {
            var CreatedBy = User.FindFirst(ClaimTypes.Email)?.Value.ToString();
            return Ok(await _helper.AddUpdateUserOrganization(model, CreatedBy, Request.Scheme, Request.Host));
        }

        /// <summary>
        /// Get Test User  By Id
        /// </summary>
        /// <param Id = Id></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetUsersOrganizationByUserId")]
        public async Task<ActionResult> GetUsersOrganizationByUserId(string UserId)
        {
            return Ok(await _helper.GetUsersOrganizationByUserId(UserId));
        }

        /// <summary>
        /// Add Functional Suite Relation
        /// </summary>
        [Authorize]
        [HttpPost("AddUpdateFunctionalSuiteRelation")]
        public async Task<ActionResult> AddUpdateFunctionalSuiteRelation(FunctionalSuiteRelation model)
        {
            return Ok(await _helper.AddUpdateFunctionalSuiteRelation(model));
        }

        /// <summary>
        /// Get Functional Suite Relation
        /// </summary>
        [Authorize]
        [HttpGet("GetFunctionalSuiteRelation")]
        public async Task<ActionResult> GetFunctionalSuiteRelation()
        {
            return Ok(await _helper.GetFunctionalSuiteRelation());
        }

        /// <summary>
        ///  Delete Functional Suite Relation By Root Id  and Parent Id
        /// </summary>
        [Authorize]
        [HttpPost("DeleteFunctionalSuiteRelation")]
        public async Task<ActionResult> DeleteFunctionalSuiteRelation(FunctionalSuiteRelation model)
        {
            return Ok(await _helper.DeleteFunctionalSuiteRelation(model));
        }

        [Authorize]
        /// <summary>
        /// 
        /// </summary>
        /// <param ApplicationId="ApplicationId"></param>
        /// <param TenantId="TenantId"></param>
        /// <param OrganizationId="OrganizationId"></param>
        /// <returns></returns>
        [HttpGet("GetTestCaseDetailsByApplicationId")]
        public async Task<ActionResult> GetTestCaseDetailsByApplicationId(int ApplicationId, Guid? TenantId, Guid? OrganizationId)
        {
            return Ok(await _helper.GetTestCaseDetailsByApplicationId(ApplicationId));
        }
    }
}