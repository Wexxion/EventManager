using System;
using RepoLayer.MessengerInterfaces;

namespace RepoLayer.Session
{
    public class BaseCommand : SessionCommand
    {
        private Func<IRequest, IResponse> ApplyFunc { get; }

        public BaseCommand(string name, IResponse helpResponse, Func<IRequest, IResponse> applyFunc, bool needAnswer = true)
            : base(name, helpResponse, needAnswer)
        {
            ApplyFunc = applyFunc;
        }
        public override IResponse Apply(IRequest message)
        {
            return ApplyFunc(message);
        }
    }
}