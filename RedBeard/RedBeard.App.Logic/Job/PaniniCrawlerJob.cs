using Quartz;
using RedBeard.Crawler;
using Quartz.Impl;
using System.Linq;
using System;
using RedBeard.Domain;

namespace RedBeard.App.Logic.Job
{
    public sealed class PaniniCrawlerJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            string[] keys = dataMap.Keys.ToArray<string>();

            foreach (string key in keys)
            {
                PaniniAlertEntry entry = (PaniniAlertEntry)dataMap[key];

                foreach (string link in entry.Links)
                {
                    // Gets the last release os a specified manga:
                    Manga c = new PaniniCrawler(link)
                        .GetLast();

                    // Sends an email with release information:
                    PaniniAlert alert = new PaniniAlert(entry.Email);
                    alert.SendIt(c);
                }
            }
        }
        public static void Run(PaniniAlertEntry [] entries, int gapInHours = 24)
        {
            // Construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // Get a scheduler, start the schedular before triggers or anything else
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            JobDataMap map = new JobDataMap();
            
            for (int i = 0; i < entries.Length; i++)
            {
                map.Add(i.ToString(), entries[i]);
            }

            // Create job
            IJobDetail job = JobBuilder.Create<PaniniCrawlerJob>()
                    .WithIdentity("RED BEARD JOB", "RED BEARD GROUP")
                    .UsingJobData(map)
                    .Build();

            // new DateTimeOffset(DateTime.Now, new TimeSpan(15, 0, 0))

            // Create trigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("RED BEARD TRIGGER", "RED BEARD GROUP")
                .StartAt(new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0, DateTimeKind.Local)))
                .WithSimpleSchedule(x => x.WithIntervalInHours(gapInHours).RepeatForever())
                //.WithSimpleSchedule(x => x.WithIntervalInSeconds(20).RepeatForever())
                .Build();

            // Schedule the job using the job and trigger 
            sched.ScheduleJob(job, trigger);
        }
    }
}
