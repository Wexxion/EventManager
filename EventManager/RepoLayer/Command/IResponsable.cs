using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.RepoLayer.Command
{
    public interface IResponsable
    {
        string Text { get; }
    }
}
