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
        public static void Main(string[] args)
        {
            ReadXMLFile readXMLFile = new ReadXMLFile();
            readXMLFile.RemoveMultipleElement();
            readXMLFile.AutoIncrementID(Constants.newXmlFile, "//Client/Registrator", "RegistratorID");
            RegistratorList registratorLists = new RegistratorList();
            GenerateXmlErr generateXmlErr = new GenerateXmlErr();
            generateXmlErr.GenErrXml();
            registratorLists.GenerateRegistratorList(Constants.newXmlFile);
            readXMLFile.AutoIncrementID(Constants.registratorXmlList, "//Registrator", "ID");
        }
    }
}