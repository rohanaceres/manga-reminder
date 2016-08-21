using Quartz;
using RedBeard.Crawler.Model;
using RedBeard.Crawler;
using Quartz.Impl;

namespace RedBeard.App.Job
{
    internal sealed class PaniniCrawlerJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            // Gets the last release os a specified manga:
            Manga c = new PaniniCrawler(@"http://www.paninicomics.com.br/web/guest/titulos_detail?category_id=201055")
                .GetLast();

            // Sends an email with release information:
            PaniniAlert alert = new PaniniAlert("ceres.rohana@gmail.com");
            alert.SendIt(c);
        }

        public static void Run(int gap = 60)
        {
            // Construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // Get a scheduler, start the schedular before triggers or anything else
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // Create job
            IJobDetail job = JobBuilder.Create<PaniniCrawlerJob>()
                    .WithIdentity("RED BEARD JOB", "RED BEARD GROUP")
                    .Build();

            // Create trigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("RED BEARD TRIGGER", "RED BEARD GROUP")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(gap).RepeatForever())
                .Build();

            // Schedule the job using the job and trigger 
            sched.ScheduleJob(job, trigger);
        }
    }
}
