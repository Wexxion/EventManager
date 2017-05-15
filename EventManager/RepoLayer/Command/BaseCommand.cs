using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaskManager.RepoLayer.MessagerInterfaces;

namespace TaskManager.RepoLayer.Command
{
    public abstract class BaseCommand
    {
        public string Name { get; }
        public event EventHandler ExecuteEvent;
        public Dictionary<BaseCommandPattern, MethodBase> MethodsDict { get; }
        protected BaseCommand(string name)
        {
            Name = name;
            MethodsDict = new Dictionary<BaseCommandPattern, MethodBase>();
            foreach (var method in GetType().GetMethods())
            {
                var attribute = method
                    .GetCustomAttribute(typeof(PatternAttribute)) as PatternAttribute;
                if (attribute == null) continue;
                if (method.GetParameters().Length != 1)
                    throw new ArgumentException("Attributed method should have only one parameter!");
                if (method.GetParameters().First().ParameterType != typeof(IncomingMessage))
                    throw new ArgumentException("Attributed method parameter should be MessageType!");
                if (!method.ReturnType.GetInterfaces().Contains(typeof(IResponsable)))
                    throw new ArgumentException("Attributed method return type should be CommandResponse!");
                MethodsDict.Add(attribute.Pattern, method);
            }
        }

        public IResponsable Execute(IncomingMessage message)
        {
            ExecuteEvent?.Invoke(this, EventArgs.Empty);
            foreach (var pattern in MethodsDict.Keys)
                if (pattern.DoesPatternAcceptArguments(message.Args))
                    return (IResponsable) MethodsDict[pattern].Invoke(this, new object[] {message});
            throw new ArgumentException("This command doesn't accept such arguments!");
        }
    }
}
