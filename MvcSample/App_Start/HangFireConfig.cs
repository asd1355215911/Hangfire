﻿using System;

using HangFire;
using HangFire.Web;

[assembly: WebActivatorEx.PostApplicationStartMethod(
    typeof(MvcSample.HangFireConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(
    typeof(MvcSample.HangFireConfig), "Stop")]

namespace MvcSample
{
    public class HangFireConfig
    {
        private static AspNetBackgroundJobServer _server;

        public static void Start()
        {
            // If you have custom Redis installation, use the
            // following method to configure HangFire:
            JobStorage.Configure(x => { x.RedisDb = 3; });
            
            _server = new AspNetBackgroundJobServer
            {
                ServerName = Environment.MachineName,
                QueueName = "default",
                WorkersCount = Environment.ProcessorCount * 2
            };

            GlobalJobFilters.Filters.Add(new PreserveCultureFilter());

            //_server.Start();
        }

        public static void Stop()
        {
            _server.Stop();
        }
    }
}