using System;
using RepoLayer.MessengerInterfaces;

namespace UILayer
{
    public interface IMessengerBot
    {
        void Start();
        void Stop();
        event Action<IRequest> OnRequest;
        event Action<Exception> OnError;
    }
}
