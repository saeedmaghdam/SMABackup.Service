using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SMA.Backup.Framework;
using SMA.Backup.Service.Model;
using SMA.Backup.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SMA.Backup.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBackupHandler _backupHandler;
        private readonly ICommonUtil _commonUtil;
        private readonly ScheduleModel _scheduleModel;

        public Worker(ILogger<Worker> logger, IBackupHandler backupHandler, ICommonUtil commonUtil)
        {
            _logger = logger;
            _backupHandler = backupHandler;
            _commonUtil = commonUtil;

            var scheduleJson = System.IO.File.ReadAllText(System.IO.Path.Combine(_commonUtil.AppPath(), "schedulesettings.json"));
            _scheduleModel = JsonConvert.DeserializeObject<ScheduleModel>(scheduleJson);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await DoProcess(stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        private async Task DoProcess(CancellationToken stoppingToken)
        {
            int sleepTime = int.MaxValue;

            Func<int, int> SleepTime = (a) =>
            {
                return a < sleepTime ? a : sleepTime;
            };

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            bool executeBackup = false;
            var d = DateTime.Now;
            var now = DateTime.Now;

            if (_scheduleModel.Daily.Any())
            {
                foreach (var dailyModel in _scheduleModel.Daily)
                {
                    if (dailyModel.LastExecution == null || now.Date > dailyModel.LastExecution.Date)
                    {
                        d = new DateTime(d.Year, d.Month, d.Day, dailyModel.Hour, dailyModel.Minute, d.Second);

                        if (now.CompareTo(d) > 0)
                        {
                            executeBackup = true;
                            dailyModel.LastExecution = now;
                            sleepTime = SleepTime(86400000); // one day
                        }
                        else
                        {
                            sleepTime = SleepTime((int)((TimeSpan)(d - now)).TotalMilliseconds);
                        }
                    }
                }
            }

            if (executeBackup)
            {
                Stopwatch stopWatch = new Stopwatch();

                stopWatch.Start();
                await _backupHandler.Backup(System.IO.Path.Combine(_commonUtil.AppPath(), "backupsettings.json"));
                stopWatch.Stop();

                sleepTime -= (int)stopWatch.ElapsedMilliseconds;
            }

            await Task.Delay(sleepTime, stoppingToken);

            _logger.LogInformation("Worker finished at: {time}", DateTimeOffset.Now);
        }
    }
}
