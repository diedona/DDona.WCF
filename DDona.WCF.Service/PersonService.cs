using DDona.WCF.Infra;
using DDona.WCF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDona.WCF.BusinessService
{
    public class PersonService
    {
        private FakeDB _db;

        public IList<string> Errors;

        public PersonService()
        {
            _db = new FakeDB();
            Errors = new List<string>();
        }

        public IList<Person> Get()
        {
            return _db.People.Where(x => x.Status).ToList();
        }

        public IList<Person> Get(string Name)
        {
            return _db.People
                .Where(x => x.Name.StartsWith(Name))
                .Where(x => x.Status)
                .ToList();
        }

        public bool Add(Person Person)
        {
            if(_db.People.Count(x => x.Name.Equals(Person.Name) && x.BirthDay == Person.BirthDay) > 0)
            {
                Errors.Add("Item já cadastrado.");
                return false;
            }

            PrepareNewPerson(Person);
            _db.People.Add(Person);
            return _db.Commit();
        }

        public bool Delete(int Id)
        {
            Person Person = (_db.People
                .Where(x => x.Id == Id))
                .FirstOrDefault();

            if(Person == null)
            {
                Errors.Add("Item não encontrado.");
                return false;
            }

            if(Person.Status == false)
            {
                Errors.Add("Item já deletado.");
                return false;
            }

            Person.Status = false;
            return _db.Commit();
        }

        private int GetLastId()
        {
            return (_db.People.Max(x => x.Id));
        }

        private void PrepareNewPerson(Person Person)
        {
            Person.Id = (GetLastId() + 1);
            Person.Status = true;
        }
    }
}
