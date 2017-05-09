using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.AppLayer
{
    public class CommandPattern
    {
        public List<CommandArgumentPattern> argumentsPattern { get; }
        public CommandPattern(string pattern)
        {
            argumentsPattern = new List<CommandArgumentPattern>();
            var argsInfo = pattern
                .Replace(" ", "")
                .Split(new string[] {"]["}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Replace("]", "").Replace("[", "").Split(':')).ToList();
            foreach (var argument in argsInfo)
            {
                if (argument.Length > 2)
                    throw new ArgumentException("Arg value can contain only one ':'!");
                switch (argument[0])
                {
                    case "listed":
                        argumentsPattern.Add(new CommandArgumentPattern(argument[1].Split(',').ToList()));
                        break;
                    case "any":
                        argumentsPattern.Add(new CommandArgumentPattern(CommandPatternType.AnyString));
                        break;
                    default:
                        throw new ArgumentException("Incorrect argument pattern type!");
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(obj, this)) return true;
            var castedObj = (CommandPattern) obj;
            for (var i = 0; i < this.argumentsPattern.Count; i++)
                if (!argumentsPattern[i].Equals(castedObj.argumentsPattern[i]))
                    return false;
            return true;
        }

        protected bool Equals(CommandPattern other)
        {
            return Equals(argumentsPattern, other.argumentsPattern);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return argumentsPattern?
                           .Aggregate(397, (current, arg) => current ^ arg.GetHashCode())
                       ?? 0;
            }
        }
    }
}
