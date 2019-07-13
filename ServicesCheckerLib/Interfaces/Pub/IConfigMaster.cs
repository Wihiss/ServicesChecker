using System;
using ServicesCheckerLib.Def.Pub;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Interfaces.Pub
{
    public interface IConfigMaster
    {
        ServiceCheckerConfig LoadFromYaml(string configFile);
    }
}
