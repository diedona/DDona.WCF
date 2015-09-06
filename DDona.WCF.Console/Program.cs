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
        static void Main(string[] args)
        {
            PersonServiceClient wcfClient = new PersonServiceClient();

            short Opt = 0;
            string Name = string.Empty;

            do
            {
                System.Console.WriteLine("===============================");
                System.Console.WriteLine("======== TESTES DE WCF ========");
                System.Console.WriteLine("===============================");
                System.Console.WriteLine();
                System.Console.WriteLine("0. QUITAR");
                System.Console.WriteLine("1. LISTAR TUDO");
                System.Console.WriteLine("2. LISTAR POR NOME");

                Opt = GetShortInput();

                if (Opt < 0)
                {
                    System.Console.WriteLine("Jegue de teta curta, você entrou uma opção inválida.");
                    System.Console.ReadKey();
                    System.Console.Clear();
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
                else if(Opt == 2)
                {
                    System.Console.Write("Digite o nome: ");
                    Name = System.Console.ReadLine();

                    IList<Person> People = wcfClient.GetByName(Name);
                    ListPeopleOnScreen(People);
                }

                System.Console.ReadKey();
                System.Console.Clear();

            } while (true);
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

        private static short GetShortInput()
        {
            string Input = System.Console.ReadLine();
            short Opt;

            if (short.TryParse(Input, out Opt))
            {
                return Opt;
            }
            else
            {
                return -1;
            }
        }
    }
}
