using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace BinTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var persons = new List<Person>();
            using (IDocumentStore store = new DocumentStore()
            {
            }.Initialize())
            {
                var session = store.OpenSession(new SessionOptions()
                {
                    Database = "PersonsCore"
                });

                session.Store(new Person
                {
                    FirstName = "Ariel" + DateTime.UtcNow.Second,
                    LastName = "Ornstein"
                });

                session.SaveChanges();
                persons = session.Query<Person>().ToList();

                foreach (var person in persons)
                {
                    Console.WriteLine("Frist name {0} \nLast name: {1} ", person.FirstName, person.LastName);
                }
            }


            Console.ReadKey();

        }
    }

    public class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}