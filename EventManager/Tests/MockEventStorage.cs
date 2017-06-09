using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainLayer;
using RepoLayer;

namespace Tests
{
    class MockEventStorage: IRepository<VEvent>
    {
        private List<VEvent> data;

        public MockEventStorage()
        {
            var person1 = new Person(1, "John", "Smith", "JohnSmith");
            var person2 = new Person(2, "Adam", "Cook", "ADC");
            var person3 = new Person(3, "Bob", "Pop", "SuperBob");
            var event1 = new VEvent(person1, "Test1");
            var event2 = new VEvent(person2, "Test2");
            var event3 = new VEvent(person3, "Test3");
            data = new List<VEvent> {event1, event2, event3};
            for (var i = 0; i < data.Count; i++)
                ((VEvent)data[i]).SetDescription($"Description{i+1}");
        }
        public void Add(VEvent entity)
        {
            data.Add(entity);
        }

        public void Add(IEnumerable<VEvent> entitys)
        {
            foreach (var entity in entitys)
                data.Add(entity);
        }

        public void Update(VEvent entity)
        {
            data.Add(entity);
        }

        public VEvent Get(Expression<Func<VEvent, bool>> predicate)
        {
            var func = predicate.Compile();
            foreach (var entity in data)
                if (func.Invoke(entity))
                    return entity;
            return null;
        }

        public void Delete(Expression<Func<VEvent, bool>> predicate)
        {
            var func = predicate.Compile();
            var toDelete = data.Where(entity => func.Invoke(entity)).ToList();
            foreach (var entity in toDelete)
                data.Remove(entity);
        }

        public IEnumerable<VEvent> GetAll()
        {
            foreach (var entity in data)
                yield return entity;
        }

        public IEnumerable<VEvent> GetAll(Expression<Func<VEvent, bool>> predicate)
        {
            var func = predicate.Compile();
            foreach (var entity in data)
                if (func.Invoke(entity))
                    yield return entity;
        }
    }
}
