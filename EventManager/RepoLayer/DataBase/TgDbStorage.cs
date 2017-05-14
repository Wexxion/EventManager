using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.DataBase.DbModel;

namespace TaskManager.RepoLayer.DataBase
{
    public class Storage
    {
        private Context Db { get; }
        public Storage()
        {
            this.Db = new Context("TestBot");
        }

        public void AddUser(Person user)
        {
            var dbUser = Db.Users.Find(user.TelegramId);
            if (dbUser == null)
            { 
                Db.Users.Add(Mapper.Map<PersonInDb>(user));
                Db.SaveChanges();
            }
        }
        public void AddEvent(VEvent e)
        {
            var dbUser = GetUser(e.Creator.TelegramId);
            var dbEvent = Mapper.Map<VEventInDb>(e);
            dbEvent.Creator = dbUser;
            foreach (var user in e.Participants)
                dbEvent.Participants.Add(GetUser(user.TelegramId));
            Db.Events.Add(dbEvent);
            Db.SaveChanges();
        }
        public PersonInDb GetUser(int id)
        {
            return Db
                 .Users
                 .Find(id);
        }
        public List<VEvent> GetAllUserEvent(int telegramId)
        {
            return Db
                .Events
                .AsEnumerable()
                .Where(x => x.Creator.TelegramId == telegramId)
                .Select(Mapper.Map<VEvent>)
                .ToList();
        }

    }
}
