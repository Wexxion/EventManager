using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.RepoLayer.Command
{
    public class BaseCommandPattern
    {
        public List<ArgumentPattern> ArgumentsPattern { get; }

        public BaseCommandPattern(params Type[] args)
        {
            var argPattern = new List<ArgumentPattern>();
            var stringArrayFlag = false;
            foreach (var arg in args)
            {
                if (stringArrayFlag) throw new ArgumentException("'Enum' can't stay after 'string[]'!");
                if (arg.IsEnum)
                {
                    var enumValues = Enum.GetValues(arg);
                    var valuesList = (
                        from object value in enumValues select value.ToString().ToLower())
                        .ToList();
                    argPattern.Add(new ArgumentPattern(valuesList));
                }
                else if (arg == typeof(string[]))
                {
                    stringArrayFlag = true;
                    argPattern.Add(new ArgumentPattern(PatternType.AnyString));
                }
                else
                    throw new ArgumentException(
                        "Only 'Enum' or 'string[]' types are acceptable in pattern!");

            }
            ArgumentsPattern = argPattern;
        }

        public bool DoesPatternAcceptArguments(List<string> args)
        {
            if (ArgumentsPattern.Count > args.Count) return false;
            for (var i = 0; i < ArgumentsPattern.Count; i++)
            {
                if (ArgumentsPattern[i].Type == PatternType.AnyString)
                    return true;
                if (!ArgumentsPattern[i].AvaliableArguments.Contains(args[i].ToLower()))
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(obj, this)) return true;
            var castedObj = (BaseCommandPattern) obj;
            if (ReferenceEquals(ArgumentsPattern, castedObj.ArgumentsPattern)) return true;
            return ArgumentsPattern?.SequenceEqual(castedObj.ArgumentsPattern) ?? false;
        }

        protected bool Equals(BaseCommandPattern other)
        {
            return Equals(ArgumentsPattern, other.ArgumentsPattern);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ArgumentsPattern?.Aggregate(397, (current, arg) => current ^ arg.GetHashCode()) ?? 0;
            }
        }
    }
}
