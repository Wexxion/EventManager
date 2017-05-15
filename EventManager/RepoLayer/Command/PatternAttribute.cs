using System;
using System.Collections.Generic;


namespace TaskManager.RepoLayer.Command
{
    public class PatternAttribute: Attribute
    {
        public BaseCommandPattern Pattern { get; }

        public PatternAttribute(params Type[] types)
        {
            Pattern = new BaseCommandPattern(types);
        }
    }
}
