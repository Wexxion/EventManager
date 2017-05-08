using System;
using System.Collections.Generic;
using System.Reflection;

namespace TaskManager.AppLayer
{
    class BaseCommand
    {
        public string Name { get; }
        public CommandType Type { get; }
        public List<CommandArgumentPattern> CommandPattern { get; }

        public List<Action> OnExecute { get; }

        public Dictionary<CommandArgumentPattern, MethodBase> MethodsDict { get; }

        public BaseCommand()
        {
            this.MethodsDict = new Dictionary<CommandArgumentPattern, MethodBase>();
            var methods = GetType().GetMethods();
            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute(typeof(PatternAttribute)) as PatternAttribute;
                if (attribute != null)
                    MethodsDict.Add(attribute.Pattern, method);
            }
        }
        public void Execute(List<string> arguments)
        {
            foreach (var action in OnExecute)
                action();
            //TODO -> смотреть ключи словаря методов и сравнивать с ними список аргументов -> получать метод
            this.MethodsDict[new CommandArgumentPattern("не нью, а ключ словаря! (туду)")]
                .Invoke(this, new object[] {arguments});
        }

        [Pattern("listed: one, two, three; any; ")] //awaiting for <command> one blablabla
        public void HandlePattern1()
        {
            
        }


    }
}
