using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RepoLayer;
using DomainLayer;

namespace Tests
{
    class MockPersonsStorage : IRepository<Person>
    {
        private List<Person> data;

        public MockPersonsStorage()
        {
            var person1 = new Person(1, "John", "Smith", "JohnSmith");
            var person2 = new Person(2, "Adam", "Cook", "ADC");
            var person3 = new Person(3, "Bob", "Pop", "SuperBob");
            data = new List<Person> {person1, person2, person3};
        }
        public void Add(Person entity)
        {
           data.Add(entity);
        }

        public void Add(IEnumerable<Person> entitys)
        {
            foreach (var entity in entitys)
                data.Add(entity);
        }

        public void Update(Person entity)
        {
            data.Add(entity);
        }

        public Person Get(Expression<Func<Person, bool>> predicate)
        {
            var func = predicate.Compile();
            foreach (var entity in data)
                if (func.Invoke(entity))
                    return entity;
            return null;
        }

        public void Delete(Expression<Func<Person, bool>> predicate)
        {
            var func = predicate.Compile();
            foreach (var entity in data)
                if (func.Invoke(entity))
                    data.Remove(entity);
        }

        public IEnumerable<Person> GetAll()
        {
            foreach (var entity in data)
                yield return entity;
        }

        public IEnumerable<Person> GetAll(Expression<Func<Person, bool>> predicate)
        {
            var func = predicate.Compile();
            foreach (var entity in data)
                if (func.Invoke(entity))
                    yield return entity;
        }
    }
}
