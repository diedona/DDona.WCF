using DDona.WCF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDona.WCF.Infra
{
    public class FakeDB
    {
        private const string FOLDER_NAME = "DataStorage";
        private const string DB_NAME = "DB.json";

        private string FolderPath;
        private string FullPath;

        public IList<Person> People;

        public FakeDB()
        {
            SetPaths();
            LoadContentFromFile();
        }

        public bool Commit()
        {
            string strObj = JsonConvert.SerializeObject(People, Formatting.None);

            SetFolder();

            using (FileStream fw = File.OpenWrite(FullPath))
            {
                byte[] byteObj = Encoding.UTF8.GetBytes(strObj);
                fw.Seek(0, SeekOrigin.Begin);
                fw.Write(byteObj, 0, byteObj.Count());
                fw.Flush();
            }

            return true;
        }

        private void SetFolder()
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
        }

        private bool LoadContentFromFile()
        {
            SetFolder();
            string strJson = File.ReadAllText(FullPath);
            People = JsonConvert.DeserializeObject<List<Person>>(strJson);

            return true;
        }

        private void SetPaths()
        {
            this.FolderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FOLDER_NAME);
            this.FullPath = System.IO.Path.Combine(FolderPath, DB_NAME);
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
