using ServicesCheckerLib.Def;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesCheckerLib.Interfaces.Pub
{
    public interface IConfigMaster
    {
        SCConfig LoadFromYaml(string configFile);
    }
}
