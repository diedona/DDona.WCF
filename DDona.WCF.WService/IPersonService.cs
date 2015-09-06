using DDona.WCF.Model;
using DDona.WCF.WService.Custom;
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
        [OperationContract]
        bool ExcludePerson(int Id);
        [OperationContract]
        StubClass GetStub();
    }
}