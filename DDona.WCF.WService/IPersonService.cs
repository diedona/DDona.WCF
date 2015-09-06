using DDona.WCF.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace DDona.WCF.WService
{
    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        bool SavePerson(Person Person);
        [OperationContract]
        IList<Person> GetAll();
        [OperationContract]
        IList<Person> GetByName(string Name);
    }
}