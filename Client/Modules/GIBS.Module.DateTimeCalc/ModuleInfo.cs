using Oqtane.Models;
using Oqtane.Modules;

namespace GIBS.Module.DateTimeCalc
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "DateTimeCalc",
            Description = "Date Time Calculator",
            Version = "1.0.0",
            ServerManagerType = "GIBS.Module.DateTimeCalc.Manager.DateTimeCalcManager, GIBS.Module.DateTimeCalc.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "GIBS.Module.DateTimeCalc.Shared.Oqtane",
            PackageName = "GIBS.Module.DateTimeCalc" 
        };
    }
}
