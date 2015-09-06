using DDona.WCF.Console.PersonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDona.WCF.Console
{
    class Program
    {
        /*
         * ESTE PROJETO SÓ REFERENCIA O WSERVICE
         * OS MODELS QUE USAMOS >NÃO< SÃO DO DDONA.WCF.MODEL, SÃO OS PROXIES QUE O DDONA.WCF.WSERVICE GEROU!
         */

        private static bool Finish = false;
        private static PersonServiceClient wcfClient = new PersonServiceClient();

        static void Main(string[] args)
        {
            short Opt = 0;
            bool Error = false;

            System.Console.WriteLine("===============================");
            System.Console.WriteLine("======== TESTES DE WCF ========");
            System.Console.WriteLine("===============================");
            System.Console.WriteLine();
            System.Console.WriteLine("0. QUITAR");
            System.Console.WriteLine("1. LISTAR TUDO");
            System.Console.WriteLine("2. LISTAR POR NOME");
            System.Console.WriteLine("3. DELETAR POR ID");
            System.Console.WriteLine("4. DADOS ALEATÓRIOS");
            System.Console.WriteLine("5. INSERIR");

            do
            {
                System.Console.WriteLine();
                System.Console.Write("Digite a opção: ");
                Opt = ConvertInput<short>(out Error);
                ChoseOption(Opt, Error);

            } while (!Finish);
        }

        private static void ChoseOption(short Opt, bool Error)
        {
            if (Opt < 0 || Opt > 5 || Error)
            {
                WrongOutputWarning();
                return;
            }

            switch (Opt)
            {
                case 0:
                    Quit();
                    break;
                case 1:
                    ListPeople();
                    break;
                case 2:
                    ListPeopleByName();
                    break;
                case 3:
                    DeleteById();
                    break;
                case 4:
                    GetStubData();
                    break;
                case 5:
                    InsertPerson();
                    break;
                default:
                    break;
            }
        }

        private static void Quit()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Adeus...");
            System.Console.WriteLine();
            Finish = true;
        }

        private static void ListPeople()
        {
            IList<Person> People = wcfClient.GetAll();
            ListPeopleOnScreen(People);
        }

        private static void ListPeopleByName()
        {
            System.Console.Write("Digite o nome: ");
            string Name = System.Console.ReadLine();

            IList<Person> People = wcfClient.GetByName(Name);
            ListPeopleOnScreen(People);
        }

        private static void DeleteById()
        {
            bool Error = false;
            int Id = 0;

            System.Console.Write("Digite o ID: ");
            Id = ConvertInput<int>(out Error);

            if (Error)
            {
                WrongOutputWarning();
                return;
            }

            if (wcfClient.ExcludePerson(Id))
            {
                System.Console.WriteLine("{0} Deletado com sucesso!", Id);
            }
            else
            {
                System.Console.WriteLine("Falha ao deletar");
            }
        }

        private static void GetStubData()
        {
            StubClass Stub = wcfClient.GetStub();
            ShowStubData(Stub);
        }

        private static void InsertPerson()
        {
            string Name;
            DateTime BirthDay;
            string Quote;
            bool Error = false;

            System.Console.Write("Nome: ");
            Name = System.Console.ReadLine();

            do
            {
                System.Console.Write("Data de Aniversário: (dd/mm/yyyy) ");
                BirthDay = ConvertInput<DateTime>(out Error);
            } while (Error);

            System.Console.Write("Citação: ");
            Quote = System.Console.ReadLine();

            Person Person = new Person()
            {
                Name = Name,
                Quote = Quote,
                BirthDay = BirthDay
            };

            if(!wcfClient.SavePerson(Person))
            {
                System.Console.WriteLine("Falha ao incluir pessoa!");
            }
            else
            {
                System.Console.WriteLine("'{0}' incluído com sucesso!", Person.Name);
            }
        }

        private static void ShowStubData(StubClass Stub)
        {
            System.Console.WriteLine();

            System.Console.WriteLine("Data: {0}, Número: {1}", Stub.CurrentDateTime.ToString(), Stub.RandomNumber);
        }

        private static void WrongOutputWarning()
        {
            System.Console.WriteLine("Jegue de teta curta, você entrou uma opção inválida.");
        }

        private static void ListPeopleOnScreen(IList<Person> People)
        {
            System.Console.WriteLine();

            foreach (Person Person in People)
            {
                System.Console.WriteLine("{0} - {1}, Data de Nascimento: {2}, Citação: {3}",
                    Person.Id, Person.Name, Person.BirthDay.ToShortDateString(), Person.Quote);
            }
        }

        private static T ConvertInput<T>(out bool Error)
        {
            string Input = System.Console.ReadLine();
            Error = false;

            try
            {
                return (T)Convert.ChangeType(Input, typeof(T));
            }
            catch (Exception)
            {
                Error = true;
                return default(T);
            }
        }
    }
}
