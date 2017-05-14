using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DomainLayer;

namespace TaskManager.RepoLayer
{
    public interface ITgStorage
    {
        void AddUser(Person user);
        void AddEvent(VEvent e);
        Person GetUser(int id);
        List<VEvent> GetAllUserEvent(int telegramId);
    }
}
