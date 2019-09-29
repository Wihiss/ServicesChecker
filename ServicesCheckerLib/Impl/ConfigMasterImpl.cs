using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ServicesCheckerLib.Def;
using ServicesCheckerLib.Def.Pub;
using ServicesCheckerLib.Interfaces.Pub;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ServicesCheckerLib.Impl.Config
{
    internal class ConfigMasterImpl : IConfigMaster
    {
        public ServiceCheckerConfig LoadFromYaml(string configFile)
        {
            return new DeserializerBuilder().
                WithNamingConvention(CamelCaseNamingConvention.Instance).
                Build().
                Deserialize<ServiceCheckerConfig>(File.ReadAllText(configFile));
        }
    }
}
