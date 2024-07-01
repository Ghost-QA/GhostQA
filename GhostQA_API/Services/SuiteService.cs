using Newtonsoft.Json;
using GhostQA_API.DTO_s;
using System.Text;
using Hangfire;
using System.Net;

namespace GhostQA_API.Services
{
    public class SuiteService
    {
        public string RunSuiteAsync(Dto_SuiteRun model, string jobId)
        {
            if (DateTime.Now.ToString("dd-MM-yyyy") == model.EndDate.ToString("dd-MM-yyyy"))
            {
                RecurringJob.RemoveIfExists(jobId);
                return $"Job '{jobId}' has expired.";
            }

            Dto_Execution data = new Dto_Execution()
            {
                testSuiteName = model.SuiteName,
                userId = model.UserId
            };

            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Api-TimeZone", model.Header);

                HttpResponseMessage response = httpClient.PostAsync(model.BaseUrl, content).GetAwaiter().GetResult();

                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                return result;
            }
        }

        public string RunSuite(string BaseUrl, string SuiteName, string UserId, int RootId, DateTime EndDate, string Header, string jobId)
        {
            Dto_SuiteRun suiteRun = new Dto_SuiteRun()
            {
                BaseUrl = BaseUrl,
                SuiteName = SuiteName,
                UserId = UserId,
                EndDate = EndDate,
                RootId = RootId,
                Header = Header
            };

            return RunSuiteAsync(suiteRun, jobId);
        }

        public void StartRecurringJob(string BaseUrl, Dto_SuiteScheduledInfo schedulingInfo, SuiteService _service, DateTime utcScheduledTime, string timeZoneHeader)
        {
            switch (schedulingInfo.Interval)
            {
                case "Every Weekday (Mon - Fri)":
                    BackgroundJob.Schedule(() => _service.RunSuite(BaseUrl, schedulingInfo.SuiteName, schedulingInfo.CreatedBy, schedulingInfo.RootId, schedulingInfo.EndDate, timeZoneHeader.ToString(), schedulingInfo.JobId), new DateTime(schedulingInfo.StartDateTime.Year, schedulingInfo.StartDateTime.Month, schedulingInfo.StartDateTime.Day, schedulingInfo.StartDateTime.Hour, schedulingInfo.StartDateTime.Minute, 0));

                    RecurringJob.AddOrUpdate(
                        schedulingInfo.JobId,
                        () => _service.RunSuite(BaseUrl, schedulingInfo.SuiteName, schedulingInfo.CreatedBy, schedulingInfo.RootId, schedulingInfo.EndDate, timeZoneHeader, schedulingInfo.JobId),
                        GetWeekdayCronExpression(utcScheduledTime));
                    break;
                case "Daily":
                    BackgroundJob.Schedule(() => _service.RunSuite(BaseUrl, schedulingInfo.SuiteName, schedulingInfo.CreatedBy, schedulingInfo.RootId, schedulingInfo.EndDate, timeZoneHeader.ToString(), schedulingInfo.JobId), new DateTime(schedulingInfo.StartDateTime.Year, schedulingInfo.StartDateTime.Month, schedulingInfo.StartDateTime.Day, schedulingInfo.StartDateTime.Hour, schedulingInfo.StartDateTime.Minute, 0));

                    RecurringJob.AddOrUpdate(
                         schedulingInfo.JobId,
                        () => _service.RunSuite(BaseUrl, schedulingInfo.SuiteName, schedulingInfo.CreatedBy, schedulingInfo.RootId, schedulingInfo.EndDate, timeZoneHeader, schedulingInfo.JobId),
                        GetDailyCronExpression(utcScheduledTime));
                    break;
                case "Weekly":
                    BackgroundJob.Schedule(() => _service.RunSuite(BaseUrl, schedulingInfo.SuiteName, schedulingInfo.CreatedBy, schedulingInfo.RootId, schedulingInfo.EndDate, timeZoneHeader.ToString(), schedulingInfo.JobId), new DateTime(schedulingInfo.StartDateTime.Year, schedulingInfo.StartDateTime.Month, schedulingInfo.StartDateTime.Day, schedulingInfo.StartDateTime.Hour, schedulingInfo.StartDateTime.Minute, 0));

                    foreach (var day in schedulingInfo.DaysOfWeek)
                    {
                        RecurringJob.AddOrUpdate(
                        schedulingInfo.JobId,
                       () => _service.RunSuite(BaseUrl, schedulingInfo.SuiteName, schedulingInfo.CreatedBy, schedulingInfo.RootId, schedulingInfo.EndDate, timeZoneHeader, schedulingInfo.JobId),
                       GetWeeklyCronExpression(day, utcScheduledTime));
                    }
                    break;
                case "Monthly":
                    BackgroundJob.Schedule(() => _service.RunSuite(BaseUrl, schedulingInfo.SuiteName, schedulingInfo.CreatedBy, schedulingInfo.RootId, schedulingInfo.EndDate, timeZoneHeader.ToString(), schedulingInfo.JobId), new DateTime(schedulingInfo.StartDateTime.Year, schedulingInfo.StartDateTime.Month, schedulingInfo.StartDateTime.Day, schedulingInfo.StartDateTime.Hour, schedulingInfo.StartDateTime.Minute, 0));

                    RecurringJob.AddOrUpdate(
                         schedulingInfo.JobId,
                        () => _service.RunSuite(BaseUrl, schedulingInfo.SuiteName, schedulingInfo.CreatedBy, schedulingInfo.RootId, schedulingInfo.EndDate, timeZoneHeader, schedulingInfo.JobId),
                        GetMonthlyCronExpression(utcScheduledTime));
                    break;
                case "Custom":
                    BackgroundJob.Schedule(() => _service.RunSuite(BaseUrl, schedulingInfo.SuiteName, schedulingInfo.CreatedBy, schedulingInfo.RootId, schedulingInfo.EndDate, timeZoneHeader.ToString(), schedulingInfo.JobId), new DateTime(schedulingInfo.StartDateTime.Year, schedulingInfo.StartDateTime.Month, schedulingInfo.StartDateTime.Day, schedulingInfo.StartDateTime.Hour, schedulingInfo.StartDateTime.Minute, 0));

                    RecurringJob.AddOrUpdate(
                         schedulingInfo.JobId,
                        () => _service.RunSuite(BaseUrl, schedulingInfo.SuiteName, schedulingInfo.CreatedBy, schedulingInfo.RootId, schedulingInfo.EndDate, timeZoneHeader, schedulingInfo.JobId),
                        GetCustomCronExpression(utcScheduledTime, schedulingInfo.RepeatEvery));
                    break;
                default:
                    throw new ArgumentException("Invalid schedule type");
            }
        }

        public string GetWeekdayCronExpression(DateTime startDate)
        {
            var hour = startDate.Hour;
            var minute = startDate.Minute;
            // Cron expression to run at specific hour and minute every weekday (Monday to Friday)
            string cronExpression = $"{minute} {hour} * * 1-5";
            return cronExpression;
        }

        public string GetDailyCronExpression(DateTime startDate)
        {
            var hour = startDate.Hour;
            var minute = startDate.Minute;
            // Cron expression to run at specific hour and minute every day
            string cronExpression = $"{minute} {hour} * * *";
            return cronExpression;
        }

        public string GetWeeklyCronExpression(DayOfWeek dayOfWeek, DateTime initialDate)
        {
            var hour = initialDate.Hour;
            var minute = initialDate.Minute;
            // Cron expression to run at specific hour and minute on the specific day of the week
            string cronExpression = $"{minute} {hour} * * {(int)dayOfWeek}";
            return cronExpression;
        }

        public string GetMonthlyCronExpression(DateTime startDate)
        {
            var dayOfMonth = startDate.Day;
            var hour = startDate.Hour;
            var minute = startDate.Minute;
            // Cron expression to run at specific hour and minute on the same day of the month
            string cronExpression = $"{minute} {hour} {dayOfMonth} * *";
            return cronExpression;
        }

        private string GetCustomCronExpression(DateTime startDate, int repeatEvery)
        {
            var hour = startDate.Hour;
            var minute = startDate.Minute;
            string cronExpression;

            if (repeatEvery < 1)
            {
                throw new ArgumentException("Repeat interval must be at least 1.");
            }

            // Custom logic based on repeat interval type
            if (repeatEvery == 1)
            {
                // Repeat every day
                cronExpression = $"{minute} {hour} * * *";
            }
            else if (repeatEvery % 7 == 0)
            {
                // Repeat every n weeks
                int weeks = repeatEvery / 7;
                cronExpression = $"{minute} {hour} */{weeks} * {((int)startDate.DayOfWeek)}";
            }
            else if (repeatEvery > 28)
            {
                // Repeat every n months
                int months = repeatEvery / 30;
                cronExpression = $"{minute} {hour} {startDate.Day} */{months} *";
            }
            else
            {
                // Repeat every n days
                cronExpression = $"{minute} {hour} */{repeatEvery} * *";
            }

            return cronExpression;
        }

        public int GetRandom()
        {
            Random random = new Random();
            int number = random.Next(1000, 10000);
            return number;
        }
    }
}
