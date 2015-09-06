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
         * OS MODELS QUE USAMOS NÃO SÃO DO DDONA.WCF.MODEL, SÃO OS PROXIES QUE O DDONA.WCF.WSERVICE GEROU!
         */ 

        static void Main(string[] args)
        {
            PersonServiceClient wcfClient = new PersonServiceClient();

            short Opt = 0;
            string Name = string.Empty;
            int Id = 0;
            bool Error = false;

            do
            {
                System.Console.WriteLine("===============================");
                System.Console.WriteLine("======== TESTES DE WCF ========");
                System.Console.WriteLine("===============================");
                System.Console.WriteLine();
                System.Console.WriteLine("0. QUITAR");
                System.Console.WriteLine("1. LISTAR TUDO");
                System.Console.WriteLine("2. LISTAR POR NOME");
                System.Console.WriteLine("3. DELETAR POR ID");
                System.Console.WriteLine("4. DADOS ALEATÓRIOS");

                Opt = ConvertInput<short>(out Error);

                if (Opt < 0 || Opt > 4 || Error)
                {
                    WrongOutputWarning();
                    continue;
                }
                else if (Opt == 0)
                {
                    System.Console.WriteLine("Adeus...");
                    return;
                }
                else if (Opt == 1)
                {
                    IList<Person> People = wcfClient.GetAll();
                    ListPeopleOnScreen(People);
                }
                else if (Opt == 2)
                {
                    System.Console.Write("Digite o nome: ");
                    Name = System.Console.ReadLine();

                    IList<Person> People = wcfClient.GetByName(Name);
                    ListPeopleOnScreen(People);
                }
                else if (Opt == 3)
                {
                    System.Console.Write("Digite o ID: ");
                    Id = ConvertInput<int>(out Error);

                    if(Error)
                    {
                        WrongOutputWarning();
                        continue;
                    }
                    
                    if(wcfClient.ExcludePerson(Id))
                    {
                        System.Console.WriteLine("{0} Deletado com sucesso!", Id);
                    }
                    else
                    {
                        System.Console.WriteLine("Falha ao deletar");
                    }
                }
                else if(Opt == 4)
                {
                    StubClass Stub = wcfClient.GetStub();
                    ShowStubData(Stub);
                }

                System.Console.ReadKey();
                System.Console.Clear();

            } while (true);
        }

        private static void ShowStubData(StubClass Stub)
        {
            System.Console.WriteLine();

            System.Console.WriteLine("Data: {0}, Número: {1}", Stub.CurrentDateTime.ToString(), Stub.RandomNumber);
        }

        private static void WrongOutputWarning()
        {
            System.Console.WriteLine("Jegue de teta curta, você entrou uma opção inválida.");
            System.Console.ReadKey();
            System.Console.Clear();
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
