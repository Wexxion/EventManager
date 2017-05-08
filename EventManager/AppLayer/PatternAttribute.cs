using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.AppLayer
{
    class PatternAttribute: Attribute
    {
        public CommandArgumentPattern Pattern { get; }
        public PatternAttribute(string pattern)
        {
            this.Pattern = new CommandArgumentPattern(pattern);
        }
    }
}
