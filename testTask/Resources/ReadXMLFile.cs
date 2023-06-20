using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using testTask;
using testTask.Resources;

namespace testTask.Resources
{
    public class ReadXMLFile
    {

        public void RemoveMultipleElement()
            {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Constants.primaryXmlFile);
            XmlNodeList clientNodes = xmlDoc.SelectNodes("//Client");
            foreach (XmlNode clientNode in clientNodes)
            {
                XmlNode removeNode = clientNode.SelectSingleNode("DiasoftID");
                if (removeNode != null)
                {
                    string fio = clientNode.SelectSingleNode("FIO").InnerText.Trim();
                    string regNumber = clientNode.SelectSingleNode("RegNumber").InnerText.Trim();
                    string diasoftID = clientNode.SelectSingleNode("DiasoftID").InnerText.Trim();
                    string registrator = clientNode.SelectSingleNode("Registrator").InnerText.Trim();

                    bool isFioValid = !string.IsNullOrEmpty(fio);
                    bool isRegNumberValid = !string.IsNullOrEmpty(regNumber);
                    bool isDiasoftIdValid = !string.IsNullOrEmpty(diasoftID);
                    bool isRegistratorValid = !string.IsNullOrEmpty(registrator);
                    if (!isFioValid || !isRegNumberValid || !isDiasoftIdValid || !isRegistratorValid)
                    {
                        xmlDoc.DocumentElement.RemoveChild(clientNode);
                    }

                }
            }
            xmlDoc.Save(Constants.newXmlFile);
        }

            public void AutoIncrementID(string xmlFile, string myNode, string reg)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFile);

                XmlNodeList clientNodes = xmlDoc.SelectNodes(myNode);

                int id = 1;
                foreach (XmlNode clientNode in clientNodes)
                {
                    XmlAttribute idAttribute = xmlDoc.CreateAttribute(reg);
                    idAttribute.Value = id.ToString();
                    clientNode.Attributes.Append(idAttribute);
                    id++;
                }
                xmlDoc.Save(xmlFile);
            }
        }
    }
