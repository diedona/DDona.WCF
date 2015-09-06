using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DDona.WCF.WService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataService.svc or DataService.svc.cs at the Solution Explorer and start debugging.
    public class PersonService : IPersonService
    {
        private BusinessService.PersonService PersonBLL;

        public PersonService()
        {
            PersonBLL = new BusinessService.PersonService();
        }

        public bool SavePerson(Model.Person Person)
        {
            return PersonBLL.Add(Person);
        }

        public IList<Model.Person> GetAll()
        {
            return PersonBLL.Get();
        }

        public IList<Model.Person> GetByName(string Name)
        {
            return PersonBLL.Get(Name);
        }
    }
}
