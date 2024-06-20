using GhostQA_API.DTO_s;
using GhostQA_API.Helper;
using GhostQA_API.Models;
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
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly DBHelper _helper;

        public SchedulerController(IRecurringJobManager recurringJobManager, DBHelper helper)
        {
            _recurringJobManager = recurringJobManager;
            _helper = helper;
        }

        [HttpPost]
        public async Task<ActionResult> ScheduleJob([FromBody] SuiteScheduleInfo schedulingInfo)
        {
            Dto_SuiteRun suiteRun = new Dto_SuiteRun()
            {
                BaseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}",
                SuiteName = schedulingInfo.SuiteName,
                UserId = schedulingInfo.CreatedBy,
                Token = $"{Request.Headers.Authorization}"
            };

            if (!Request.Headers.TryGetValue("X-Api-Timezone", out StringValues timeZoneHeader))
                return BadRequest("Timezone header is missing.");

            suiteRun.Header = timeZoneHeader;

            SuiteService _service = new SuiteService();

            switch (schedulingInfo.Interval)
            {
                case "None":
                    BackgroundJob.Schedule(() => _service.RunSuite(suiteRun), TimeSpan.FromMinutes(1));
                    break;
                case "Weekdays":
                    _recurringJobManager.AddOrUpdate(
                        $"SuiteScheduleJob - {schedulingInfo.RootId}-{schedulingInfo.SuiteName}",
                        () => _service.RunSuite(suiteRun),
                        Cron.Weekly(DayOfWeek.Monday,
                        schedulingInfo.Hour, schedulingInfo.Minute));

                    _recurringJobManager.AddOrUpdate(
                        $"SuiteScheduleJob - {schedulingInfo.RootId}-{schedulingInfo.SuiteName}",
                        () => _service.RunSuite(suiteRun),
                        Cron.Weekly(DayOfWeek.Tuesday,
                        schedulingInfo.Hour, schedulingInfo.Minute));

                    _recurringJobManager.AddOrUpdate(
                        $"SuiteScheduleJob - {schedulingInfo.RootId}-{schedulingInfo.SuiteName}",
                        () => _service.RunSuite(suiteRun),
                        Cron.Weekly(DayOfWeek.Wednesday,
                        schedulingInfo.Hour, schedulingInfo.Minute));

                    _recurringJobManager.AddOrUpdate(
                        $"SuiteScheduleJob - {schedulingInfo.RootId}-{schedulingInfo.SuiteName}",
                        () => _service.RunSuite(suiteRun),
                        Cron.Weekly(DayOfWeek.Thursday,
                        schedulingInfo.Hour, schedulingInfo.Minute));

                    _recurringJobManager.AddOrUpdate(
                        $"SuiteScheduleJob - {schedulingInfo.RootId}-{schedulingInfo.SuiteName}",
                        () => _service.RunSuite(suiteRun),
                        Cron.Weekly(DayOfWeek.Friday,
                        schedulingInfo.Hour, schedulingInfo.Minute));
                    break;
                case "Daily":
                    _recurringJobManager.AddOrUpdate(
                         $"SuiteScheduleJob - {schedulingInfo.RootId}-{schedulingInfo.SuiteName}",
                        () => _service.RunSuite(suiteRun),
                        Cron.Daily(schedulingInfo.Hour, schedulingInfo.Minute));
                    break;
                case "Weekly":
                    _recurringJobManager.AddOrUpdate(
                         $"SuiteScheduleJob - {schedulingInfo.RootId}-{schedulingInfo.SuiteName}",
                        () => _service.RunSuite(suiteRun),
                        Cron.Weekly(schedulingInfo.DayOfWeek,
                        schedulingInfo.Hour, schedulingInfo.Minute));
                    break;
                case "Monthly":
                    _recurringJobManager.AddOrUpdate(
                         $"SuiteScheduleJob - {schedulingInfo.RootId}-{schedulingInfo.SuiteName}",
                        () => _service.RunSuite(suiteRun),
                        Cron.Monthly(schedulingInfo.DayOfMonth,
                        schedulingInfo.Hour, schedulingInfo.Minute));
                    break;
                case "Custom":
                    RecurringJob.RemoveIfExists($"SuiteScheduleJob - {schedulingInfo.RootId}-{schedulingInfo.SuiteName}");

                    DateTime current = schedulingInfo.StartDate;
                    while (current <= schedulingInfo.EndDate)
                    {
                        _recurringJobManager.AddOrUpdate(
                            $"SuiteScheduleJob - {schedulingInfo.RootId}-{schedulingInfo.SuiteName}-{current:yyyyMMddHHmmss}",
                            () => _service.RunSuite(suiteRun),
                            Cron.MinuteInterval(schedulingInfo.IntervalInMinutes)
                        );

                        current = current.AddMinutes(schedulingInfo.IntervalInMinutes);
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid schedule type");
            }

            await _helper.SaveSuiteSchedulerInfo(schedulingInfo);
            return Ok();
        }
    }
}
