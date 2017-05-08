using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.AppLayer
{
    class BaseCommand
    {
        public string Name { get; }
        public CommandType Type { get; }
        public List<CommandArgumentPattern> CommandPattern { get; }

        public List<Action> OnExecute { get; }

        public bool CheckCommandPattern(List<string> arguments)
        {
            if (arguments.Count == CommandPattern.Count)
        }

        public void Execute(List<string> arguments)
        {
            foreach (var action in OnExecute)
                action();
        }


    }
}
