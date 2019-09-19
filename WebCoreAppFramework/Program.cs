using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetCoreLogger;
using WebCoreAppFramework.Data;

namespace WebCoreAppFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddFileLogger();
                //logging.AddFileLogger(options =>
                //{

                //    options.Folder = @"C:\logs\" + AppDomain.CurrentDomain.FriendlyName;
                //    options.MaxFileSizeInMB = 5;
                //    options.RetainPolicyFileCount = 5;
                //    options.FileName = AppDomain.CurrentDomain.FriendlyName;
                //});
            });
    }
}
