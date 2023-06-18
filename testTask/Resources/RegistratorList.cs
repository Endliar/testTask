using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace testTask.Resources
{

    public class Registrator
    {
        public string Name { get; set; }
        public string ID { get; set; }
    }

    public class RegistratorList
    {
        const string primaryXmlFile = "C://Users//endli//source//repos//testTask//testTask//Clients.xml";
        const string xmlFile = "C://Users//endli//source//repos//testTask//testTask//example.xml";
        const string newXmlFile = "C://Users//endli//source//repos//testTask//testTask//newExample.xml";
        const string registratorXmlList = "C://Users//endli//source//repos//testTask//testTask//registratorList.xml";

            public void GenerateRegistratorList(string newXmlFile)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(newXmlFile);

                XmlNodeList clientNodes = xmlDoc.SelectNodes("//Client");

                List<Registrator> registrators = new List<Registrator>();

                foreach (XmlNode clientNode in clientNodes)
                {
                    XmlNode registratorNode = clientNode.SelectSingleNode("Registrator");
                    if (registratorNode != null)
                    {
                        string registratorName = registratorNode.InnerText;
                        string registratorId = clientNode.Attributes["RegistratorID"]?.Value;

                        registrators.Add(new Registrator { Name = registratorName, ID = registratorId });
                    }
                }

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Registrator>));
                using (FileStream stream = new FileStream(registratorXmlList, FileMode.Create))
                {
                    xmlSerializer.Serialize(stream, registrators);
                }
            }
        
    }
}
