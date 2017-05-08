using System;

namespace TaskManager.AppLayer
{
    public class PatternAttribute: Attribute
    {
        public CommandArgumentPattern Pattern { get; }
        public PatternAttribute(string pattern)
        {
            Pattern = new CommandArgumentPattern(pattern);
        }
    }
}
