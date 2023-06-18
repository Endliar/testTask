using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace testTask.Resources
{
    public class GenerateXmlErr
    {

        const string primaryXmlFile = "C://Users//endli//source//repos//testTask//testTask//Clients.xml";
        const string xmlFile = "C://Users//endli//source//repos//testTask//testTask//example.xml";
        const string newXmlFile = "C://Users//endli//source//repos//testTask//testTask//newExample.xml";
        const string registratorXmlList = "C://Users//endli//source//repos//testTask//testTask//registratorList.xml";

        public void GenErrXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(primaryXmlFile);
            XmlNodeList clientNodes = xmlDoc.SelectNodes("//Client");
            var errorCounts = new Dictionary<string, int>();
            int totalErrors = 0;

            foreach (XmlNode clientNode  in clientNodes)
            {
                XmlNode removeNode = clientNode.SelectSingleNode("DiasoftID");
                if (removeNode != null) 
                {
                    bool isFioValid = !string.IsNullOrEmpty(clientNode.SelectSingleNode("FIO")?.InnerText.Trim());
                    bool isRegNumberValid = !string.IsNullOrEmpty(clientNode.SelectSingleNode("RegNumber").InnerText.Trim());
                    bool isDisoftIDValid = !string.IsNullOrEmpty(clientNode.SelectSingleNode("DiasoftID").InnerText.Trim());
                    bool isRegistratorValid = !string.IsNullOrEmpty(clientNode.SelectSingleNode("Registrator").InnerText.Trim());

                    if (!isFioValid)
                    {
                        AddToErrorsList(errorCounts, "Не указано ФИО");
                        totalErrors++;
                    }

                    if (!isRegNumberValid)
                    {
                        AddToErrorsList(errorCounts, "Не указан рег номер");
                        totalErrors++;
                    }

                    if (!isDisoftIDValid)
                    {
                        AddToErrorsList(errorCounts, "Не указан айдишник");
                        totalErrors++;
                    }

                    if (!isRegistratorValid)
                    {
                        AddToErrorsList(errorCounts, "Не указан айдишник регистратора");
                        totalErrors++;
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter("C://Users//endli//source//repos//testTask//testTask//errorList.txt"))
            {
                foreach (var error in errorCounts.OrderByDescending(x => x.Value)) 
                {
                    writer.WriteLine($"{error.Key}: {error.Value} записей");
                }
                writer.WriteLine($"Всего ошибочных записей: {totalErrors}");
            }
        }

        public void AddToErrorsList(Dictionary<string, int> errorCounts, string errorType)
        {
            if (errorCounts.ContainsKey(errorType))
            {
                errorCounts[errorType]++;
            }
            else
            {
                errorCounts[errorType] = 1;
            }
        }
    }
}
