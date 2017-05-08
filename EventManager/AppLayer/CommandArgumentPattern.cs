using System;
using System.Collections.Generic;
using System.Linq;


namespace TaskManager.AppLayer
{
    class CommandArgumentPattern
    {
        public CommandPatternType Type { get; }
        public List<string> AvaliableArguments { get; private set; }

        public CommandArgumentPattern(CommandPatternType type)
        {
            Type = type;
        }

        public CommandArgumentPattern(List<string> avaliableArguments)
        {
            Type = CommandPatternType.ListedString;
            AvaliableArguments = avaliableArguments;
        }

        public CommandArgumentPattern(string pattern)
        {
            
        }

        public void AddArgument(string argument)
        {
            if (Type != CommandPatternType.ListedString)
                throw new ArgumentException("CommandPatternType should be Listed String!");
            AvaliableArguments.Add(argument);
        }

        public void AddArguments(List<string> arguments)
            => AvaliableArguments = AvaliableArguments.Concat(arguments).ToList();


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (!(obj is CommandArgumentPattern)) return false;
            var castedObject = (CommandArgumentPattern) obj;
            var sameArgs = castedObject.AvaliableArguments.SequenceEqual(AvaliableArguments);
            var sameTypes = castedObject.Type.Equals(Type);
            return sameArgs && sameTypes;
        }

        protected bool Equals(CommandArgumentPattern other)
        {
            return Equals(AvaliableArguments, other.AvaliableArguments);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return AvaliableArguments.Aggregate(
                    ((int)Type * 397), 
                    (current, argument) => current ^ argument.GetHashCode()
                    );
            }
        }

    }
}
