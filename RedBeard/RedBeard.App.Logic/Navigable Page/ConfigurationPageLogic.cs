using RedBeard.App.Logic.Operation;
using System;

namespace RedBeard.App.Logic.NavigablePage
{
    public sealed class ConfigurationPageLogic : AbstractNavigablePage
    {
        public override string ViewPageName { get { return "ConfigurationPage"; } }

        public ConfigurationPageLogic(NavigableService navigationService) 
            : base(navigationService) { }
    }
}
