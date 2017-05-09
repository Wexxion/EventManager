using System;
using System.Collections.Generic;

namespace TaskManager.AppLayer
{
    public class PatternAttribute: Attribute
    {
        public CommandPattern pattern { get; }
        public PatternAttribute(string pattern)
        {
            this.pattern = new CommandPattern(pattern);
        }
    }
}
