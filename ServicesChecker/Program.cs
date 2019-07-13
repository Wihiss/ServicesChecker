using ServicesCheckerLib.Def;
using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Impl;
using ServicesCheckerLib.Impl.Pub;
using ServicesCheckerLib.Interfaces.Pub;
using System;
using System.Threading;

namespace ServicesChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            /*SCConfig config = new SCConfig();

            ServiceDef sd = new ServiceDef()
            {
                ID = 1,
                Name = "Test Service",
                Host = "localhost",
                Port = 5000,
                // Host = "http://depechemode345.com",
                // Host = "http://localhost:5000/",
                PollPeriod = 10
            };

            config.Services = new ServiceDef[1] { sd };*/


            ServiceCheckerConfig config = ServiceCheckerFactory.CreateConfigMaster().LoadFromYaml("config.yaml");

            IRunner runner = ServiceCheckerFactory.CreateRunner(ComponentFactory.CreateTimeMaster(), ComponentFactory.CreateOutput(config.Output), config.Services, false);
            runner.Start();

            // Console.ReadKey();

            Thread.Sleep(60000);

            runner.Stop();
        }
    }
}
