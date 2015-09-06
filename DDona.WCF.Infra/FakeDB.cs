using DDona.WCF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDona.WCF.Infra
{
    public class FakeDB
    {
        public IList<Person> People;

        public FakeDB()
        {
            People = LoadContent();
        }

        private IList<Person> LoadContent()
        {
            return new List<Person>()
            {
                new Person
                { 
                    Id = 1, Name = "Baby Worn", BirthDay = new DateTime(1969, 7, 12), 
                    CreateDate = DateTime.Now, Quote = "Nunca se sabe, é sempre assim.", Status = true
                },
                new Person
                { 
                    Id = 2, Name = "Will Wonder", BirthDay = new DateTime(1999, 2, 05), 
                    CreateDate = DateTime.Now, Quote = "Will i ever get another chance?", Status = true
                },
                new Person
                { 
                    Id = 3, Name = "Dani Lolz", BirthDay = new DateTime(1972, 1, 25), 
                    CreateDate = DateTime.Now, Quote = @"\Eu\Tenho\Tanto\Pra\Dizer <- não é bug, tá.", Status = true
                },
                new Person
                { 
                    Id = 4, Name = "Cassandra Tend", BirthDay = new DateTime(1995, 4, 19), 
                    CreateDate = DateTime.Now, Quote = "EU SOU RYCAAAAAAAAAAA!", Status = true
                },
            };
        }
    }
}
