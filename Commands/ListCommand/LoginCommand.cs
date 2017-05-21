using System.ComponentModel.Composition;
using TaskManager.AppLayer;
using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace NewCommand
{
    [Export(typeof(BaseCommand))]
    public class LoginCommand : BaseCommand
    {
        public LoginCommand() : base("login")
        {

        }

        [Pattern(typeof(string[]))]
        public IResponse GetEventsInfo(IRequest msg)
        {
            if (msg.Args.Count != 2)
                return new BaseResponse($"Send me your name and surname");
            var message = string.Join(" ", msg.Args);

            return new BaseResponse($"Hello {message}, I remember you");
        }

    }

}
