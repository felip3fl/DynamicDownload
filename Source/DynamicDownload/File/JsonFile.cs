using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace File
{
    public class JsonFile
    {
        private const string defaultPath = "\\";
        private const string fileName = "Videos.json";

        public void Save(string path, string content){

            System.IO.File.WriteAllText(path, content);
        } 

        public string[] ReadAllLines(string path)
        {
            //check if file exists
            if (checkIfFileExists(path))
            {
                throw new System.IO.FileNotFoundException();
            }

            var fileContent = System.IO.File.ReadAllLines(path);

            return fileContent;
        }

        public string ReadAllText(string path)
        {
            //check if file exists

            //var filesWithAddress = Directory.GetFiles(defaultPath);

            if (checkIfFileExists(path))
            {
                throw new System.IO.FileNotFoundException();
            }

            var fileContent = System.IO.File.ReadAllText(fileName);

            return fileContent;
        }

        private bool checkIfFileExists(string path)
        {
            return System.IO.File.Exists(path);
        }

        public void Save(object content)
        {
            var seriazedObject = JsonConvert.SerializeObject(content, Formatting.Indented);
            
            System.IO.File.WriteAllText(defaultPath + fileName, seriazedObject);
        }

        public string Load()
        {



            var jsonObject = new JsonObject()
            {
                ["descricao"] = "Plataforma .NET",
                ["anoInicial"] = 2002,
                ["pessoasChave"] = new JsonArray("Anders Hejlsberg", "Scott Hanselman", "Mads Torgersen", "Miguel de Icaza"),
                ["ferramentas"] = new JsonObject
                {
                    ["microsoft"] = new JsonArray("Visual Studio 2019", "Visual Studio 2022", "Visual Studio Code"),
                    ["jetBrains"] = "Rider"
                }
            };


            return ReadAllText(defaultPath + fileName);
        }

    }
}
