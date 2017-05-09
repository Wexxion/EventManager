using System;
using System.Collections.Generic;
using System.Reflection;

namespace TaskManager.AppLayer
{
    public class BaseCommand
    {
        public string Name { get; }
        public CommandType Type { get; }
        public List<CommandArgumentPattern> CommandPattern { get; }
        public List<Action> OnExecute { get; }
        public Dictionary<CommandPattern, MethodBase> MethodsDict { get; }

        public BaseCommand()
        {
            MethodsDict = new Dictionary<CommandPattern, MethodBase>();
            var methods = GetType().GetMethods();
            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute(typeof(PatternAttribute)) as PatternAttribute;
                if (attribute != null)
                    MethodsDict.Add(attribute.pattern, method);
            }
        }
        public void Execute(List<string> arguments)
        {
            foreach (var action in OnExecute)
                action();
            //TODO -> смотреть ключи словаря методов и сравнивать с ними список аргументов -> получать метод
            MethodsDict[new CommandPattern("smth")]
                .Invoke(this, new object[] {arguments});
        }
    }
}
