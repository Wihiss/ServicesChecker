using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ServicesCheckerLib.Def;
using ServicesCheckerLib.Interfaces.Pub;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ServicesCheckerLib.Impl.Config
{
    internal class ConfigMasterImpl : IConfigMaster
    {
        public SCConfig LoadFromYaml(string configFile)
        {
            return new DeserializerBuilder().
                WithNamingConvention(new CamelCaseNamingConvention()).
                Build().
                Deserialize<SCConfig>(File.ReadAllText(configFile));
        }
    }
}
