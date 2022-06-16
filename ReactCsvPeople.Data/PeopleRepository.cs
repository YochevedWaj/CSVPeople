using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ReactCsvPeople.Data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            using var context = new PeopleDataContext(_connectionString);
            return context.People.ToList();
        }

        public void DeleteAll()
        {
            using var context = new PeopleDataContext(_connectionString);
            context.People.RemoveRange(context.People);
            context.SaveChanges();
        }

        public void AddMultiple(List<Person> people)
        {
            using var context = new PeopleDataContext(_connectionString);
            context.People.AddRange(people);
            context.SaveChanges();
        }

    }
}