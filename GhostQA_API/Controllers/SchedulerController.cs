using GhostQA_API.DTO_s;
using GhostQA_API.Helper;
using GhostQA_API.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace GhostQA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private readonly DBHelper _helper;

        public SchedulerController(DBHelper helper)
        {
            _helper = helper;
        }

        [HttpPost]
        public async Task<ActionResult> ScheduleJob([FromBody] Dto_SuiteScheduledInfo schedulingInfo)
        {
            string BaseUrl = $"{Request.Scheme}://{Request.Host}/api/Selenium/ExecuteTestSuite";

            if (!Request.Headers.TryGetValue("X-Api-Timezone", out StringValues timeZoneHeader))
                return BadRequest("Timezone header is missing.");

            SuiteService _service = new SuiteService();
            var random = _service.GetRandom();
            schedulingInfo.JobId = $"SuiteScheduleJob - #{random} - {schedulingInfo.RootId}-{schedulingInfo.SuiteName}";
            TimeZoneInfo localTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneHeader);
            DateTime utcScheduledTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(schedulingInfo.StartDateTime.Year, schedulingInfo.StartDateTime.Month, schedulingInfo.StartDateTime.Day, schedulingInfo.StartDateTime.Hour, schedulingInfo.StartDateTime.Minute, 0), localTimeZone);

            if (schedulingInfo.Interval == "Do Not Repeat")
                BackgroundJob.Schedule(() => _service.RunSuite(BaseUrl, schedulingInfo.SuiteName, schedulingInfo.CreatedBy, schedulingInfo.RootId, schedulingInfo.EndDate, timeZoneHeader.ToString(), schedulingInfo.JobId), new DateTime(schedulingInfo.StartDateTime.Year, schedulingInfo.StartDateTime.Month, schedulingInfo.StartDateTime.Day, schedulingInfo.StartDateTime.Hour, schedulingInfo.StartDateTime.Minute, 0));

            BackgroundJob.Schedule(() => _service.StartRecurringJob(BaseUrl, schedulingInfo, _service, utcScheduledTime, timeZoneHeader.ToString()), new DateTime(schedulingInfo.StartDateTime.Year, schedulingInfo.StartDateTime.Month, schedulingInfo.StartDateTime.Day, schedulingInfo.StartDateTime.Hour, schedulingInfo.StartDateTime.Minute, schedulingInfo.StartDateTime.Second));

            await _helper.SaveSuiteSchedulerInfo(schedulingInfo);
            return Ok();
        }
    }
}
