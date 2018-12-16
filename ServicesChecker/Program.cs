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
            SCConfig config = new SCConfig();

            ServiceDef sd = new ServiceDef()
            {
                ID = 1,
                Name = "Test Servrce",
                Host = "abc",
                Port = 111,
                PollPeriod = 10
            };

            config.Services = new ServiceDef[1] { sd };

            ISCRunner runner = SCFactory.CreateRunner(config, false);
            runner.Start();

            Console.Read();

            runner.Stop();
        }
    }
}
