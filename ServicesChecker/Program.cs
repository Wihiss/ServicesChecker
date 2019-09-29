using ServicesCheckerLib.Def;
using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Impl;
using ServicesCheckerLib.Impl.Pub;
using ServicesCheckerLib.Interfaces.Pub;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesChecker
{
    class Program
    {
        private static ManualResetEvent stopped = new ManualResetEvent(false);
        private static IRunner runner;

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

            runner = ServiceCheckerFactory.CreateRunner(ComponentFactory.CreateTimeMaster(), ComponentFactory.CreateOutput(config.Output), config.Services, false);

            // Fire and forget
            Task.Run(() =>
            {
                runner.Start();
            });

            Console.CancelKeyPress += OnCancel;

            while (true)
            {
                if (stopped.WaitOne(1000))
                    break;
            }
        }

        private static void OnCancel(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Stopping...");

            Console.CancelKeyPress -= OnCancel;

            try
            {
                runner.Stop();
            }
            catch (Exception) {}

            stopped.Set();
        }
    }
}
