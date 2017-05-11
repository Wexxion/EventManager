using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.RepoLayer.Command
{
    public class CommandPattern
    {
        public List<CommandArgumentPattern> ArgumentsPattern { get; }
        public CommandPattern(string pattern)
        {
            //values such as "[listed: x,...z]... ...[any] are accepted"
            ArgumentsPattern = new List<CommandArgumentPattern>();
            var argsInfo = pattern
                .Replace(" ", "")
                .Split(new [] {"]["}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Replace("]", "").Replace("[", "").Split(':')).ToList();
            foreach (var argument in argsInfo)
            {
                if (argument.Length > 2)
                    throw new ArgumentException("Arg value can contain only one ':'!");
                switch (argument[0])
                {
                    case "listed":
                        ArgumentsPattern.Add(new CommandArgumentPattern(argument[1].Split(',').ToList()));
                        break;
                    case "any":
                        ArgumentsPattern.Add(new CommandArgumentPattern(CommandPatternType.AnyString));
                        break;
                    default:
                        throw new ArgumentException(
                            "Incorrect argument pattern type! " +
                            "Values such as \"[listed: x,..., z] ... [any] are accepted\"!");
                }
            }
        }

        public bool DoesPatternAcceptArguments(List<string> args)
        {
            if (args.Count != ArgumentsPattern.Count) return false;
            for (var i = 0; i < args.Count; i++)
            {
                if (ArgumentsPattern[i].Type == CommandPatternType.AnyString)
                    continue;
                if (!ArgumentsPattern[i].AvaliableArguments.Contains(args[i]))
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(obj, this)) return true;
            var castedObj = (CommandPattern) obj;
            if (ReferenceEquals(this.ArgumentsPattern, castedObj.ArgumentsPattern)) return true;
            return ArgumentsPattern?.SequenceEqual(castedObj.ArgumentsPattern) ?? false;
        }

        protected bool Equals(CommandPattern other)
        {
            return Equals(ArgumentsPattern, other.ArgumentsPattern);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ArgumentsPattern?
                           .Aggregate(397, (current, arg) => current ^ arg.GetHashCode()) ?? 0;
            }
        }
    }
}
