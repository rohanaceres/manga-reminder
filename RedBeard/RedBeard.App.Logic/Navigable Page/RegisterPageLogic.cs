using RedBeard.App.Logic.Operation;

namespace RedBeard.App.Logic.NavigablePage
{
    public sealed class RegisterPageLogic : AbstractNavigablePage
    {
        public override string ViewPageName { get { return "RegisterPage"; } }

        public RegisterPageLogic(NavigableService navigationService) 
            : base(navigationService) { }
    }
}
