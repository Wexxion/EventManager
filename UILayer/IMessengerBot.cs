using System;
using System.Threading.Tasks;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.UILayer
{
    public interface IMessengerBot
    {
        void Start();
        void Stop();
        event Action<IRequest> OnRequest;
        event Action<Exception> OnError;
    }
}
