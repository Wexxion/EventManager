using System;

namespace TaskManager.AppLayer
{
    class PatternAttribute: Attribute
    {
        public CommandArgumentPattern Pattern { get; }
        public PatternAttribute(string pattern)
        {
            Pattern = new CommandArgumentPattern(pattern);
        }
    }
}
