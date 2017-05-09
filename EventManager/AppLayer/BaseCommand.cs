using System;
using System.Collections.Generic;
using System.Reflection;

namespace TaskManager.AppLayer
{
    public abstract class BaseCommand
    {
        public string Name { get; }
        public CommandType Type { get; }
        public List<Action> OnExecute { get; }
        public Dictionary<CommandPattern, MethodBase> MethodsDict { get; }

        protected BaseCommand(string name)
        {
            Name = name;
            OnExecute = new List<Action>();
            MethodsDict = new Dictionary<CommandPattern, MethodBase>();
            var methods = GetType().GetMethods();
            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute(typeof(PatternAttribute)) as PatternAttribute;
                if (attribute != null)
                {
                    if (method.ReturnType != typeof(string))
                        throw new ArgumentException("Attributed method return type should be string!");
                    MethodsDict.Add(attribute.pattern, method);
                }
            }
        }
        public string Execute(List<string> arguments)
        {
            foreach (var action in OnExecute)
                action();
            foreach (var pattern in MethodsDict.Keys)
                if (pattern.IsPatternAcceptsArguments(arguments))
                    return (string)MethodsDict[pattern].Invoke(this, new object[] { arguments });
            throw new ArgumentException("This command doesn't accept such arguments!");
        }
    }
}
