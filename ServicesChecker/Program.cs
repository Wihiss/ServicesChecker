using ServicesCheckerLib.Def;
using ServicesCheckerLib.Impl.Pub;
using ServicesCheckerLib.Interfaces.Pub;
using System;

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


            SCConfig config = SCFactory.CreateConfigMaster().LoadFromYaml("config.yaml");

            ISCRunner runner = SCFactory.CreateRunner(config, false);
            runner.Start();

            Console.ReadKey();

            runner.Stop();
        }
    }
}
