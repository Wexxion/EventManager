using System;
using System.Collections.Generic;
using System.Linq;


namespace TaskManager.RepoLayer.Command
{
    public class ArgumentPattern
    {
        public PatternType Type { get; }
        public List<string> AvaliableArguments { get; }
        public ArgumentPattern(PatternType type)
        {
            Type = type;
            AvaliableArguments = new List<string>();
        }

        public ArgumentPattern(List<string> avaliableArguments)
        {
            Type = PatternType.ListedString;
            AvaliableArguments = avaliableArguments;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (!(obj is ArgumentPattern)) return false;
            var castedObject = (ArgumentPattern) obj;
            if (ReferenceEquals(AvaliableArguments, castedObject.AvaliableArguments))
                return castedObject.Type.Equals(Type);
            var sameArgs = castedObject.AvaliableArguments?.OrderBy(x => x)
                .SequenceEqual(AvaliableArguments.OrderBy(x => x)) ?? false;
            var sameTypes = castedObject.Type.Equals(Type);
            return sameArgs && sameTypes;
        }

        protected bool Equals(ArgumentPattern other)
        {
            return Equals(AvaliableArguments, other.AvaliableArguments);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return AvaliableArguments.OrderBy(x => x).Aggregate(
                    ((int)Type * 397), 
                    (current, argument) => current ^ argument.GetHashCode()
                    );
            }
        }

    }
}
