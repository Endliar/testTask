using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using testTask.Resources;

namespace testTask
{
    public class Program
    {

        const string primaryXmlFile = "C://Users//endli//source//repos//testTask//testTask//Clients.xml";
        const string xmlFile = "C://Users//endli//source//repos//testTask//testTask//example.xml";
        const string newXmlFile = "C://Users//endli//source//repos//testTask//testTask//newExample.xml";
        const string registratorXmlList = "C://Users//endli//source//repos//testTask//testTask//registratorList.xml";

        public static void Main(string[] args)
        {
            ReadXMLFile readXMLFile = new ReadXMLFile();
            readXMLFile.RemoveMultipleElement();
            readXMLFile.AutoIncrementID(newXmlFile, "//Client/Registrator", "RegistratorID");
            RegistratorList registratorLists = new RegistratorList();
            GenerateXmlErr generateXmlErr = new GenerateXmlErr();
            generateXmlErr.GenErrXml();
            registratorLists.GenerateRegistratorList(newXmlFile);
            readXMLFile.AutoIncrementID(registratorXmlList, "//Registrator", "ID");
        }
    }
}