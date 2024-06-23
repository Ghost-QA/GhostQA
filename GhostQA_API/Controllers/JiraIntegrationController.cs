using GhostQA_API.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace GhostQA_API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class JiraIntegrationController : ControllerBase
    {
        private readonly DBHelper _helper;

        public JiraIntegrationController(DBHelper helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// Get Jira Project details with Test Cases and Labels
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetProjectDetailswithTestCase")]
        public async Task<ActionResult> GetProjectDetailswithTestCase(string userId)
        {
            return Ok(await _helper.GetJiraProjectDetails(userId));
        }
    }
}