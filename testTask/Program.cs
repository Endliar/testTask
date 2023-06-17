using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace testTask
{
    public class Program
    {
        const string xmlFile = "C://Users//endli//source//repos//testTask//testTask//example.xml";
        const string newXmlFile = "C://Users//endli//source//repos//testTask//testTask//newExample.xml";
        class ReadXMLFile
        {
            public void RemoveMultipleElement()
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFile);

                XmlNodeList clientNodes = xmlDoc.SelectNodes("//Client");

                foreach (XmlNode clientNode in clientNodes)
                {
                    string fio = clientNode.SelectSingleNode("FIO").InnerText;
                    string regNumber = clientNode.SelectSingleNode("RegNumber").InnerText;
                    string diasoftID = clientNode.SelectSingleNode("DiasoftID").InnerText;
                    string registrator = clientNode.SelectSingleNode("Registrator").InnerText;

                    bool isFioValid = Regex.IsMatch(fio, @"^[А-Яа-я\s]+$");
                    bool isRegNumberValid = Regex.IsMatch(regNumber, @"^\d{2,4}$");
                    bool isDiasoftIdValid = Regex.IsMatch(diasoftID, @"^\d{13}$");
                    bool isRegistratorValid = Regex.IsMatch(registrator, @"^[А-Яа-я]+\s[А-Яа-я]\.\s?[А-Яа-я]\.$");

                    if (!isFioValid || !isRegNumberValid || !isDiasoftIdValid || !isRegistratorValid)
                    {
                        xmlDoc.DocumentElement.RemoveChild(clientNode);
                    }
                }
                xmlDoc.Save(newXmlFile);
            }

            public void AutoIncrementID()
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(newXmlFile);

                XmlNodeList clientNodes = xmlDoc.SelectNodes("//Client/Registrator");

                int id = 1;
                foreach (XmlNode clientNode in clientNodes)
                {
                    XmlAttribute idAttribute = xmlDoc.CreateAttribute("RegistratorID");
                    idAttribute.Value = id.ToString();
                    clientNode.Attributes.Append(idAttribute);
                    id++;
                }
                xmlDoc.Save(newXmlFile);
            }
        }

        public class Registrator
        {
            public string Name { get; set; }
            public string ID { get; set; }
        }

        public class RegistratorList
        {
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
                using(FileStream stream = new FileStream("registratorList.xml", FileMode.Create))
                {
                    xmlSerializer.Serialize(stream, registrators);
                }
            }
        }

        public static void Main(string[] args)
        {
            ReadXMLFile readXMLFile = new ReadXMLFile();
            readXMLFile.RemoveMultipleElement();
            readXMLFile.AutoIncrementID();

            RegistratorList registratorList = new RegistratorList();
            registratorList.GenerateRegistratorList(newXmlFile);
        }
    }
}