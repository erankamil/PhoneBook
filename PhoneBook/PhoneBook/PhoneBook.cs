using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PhoneBook
{
    public class PhoneBook
    {
        public string filepath { get; set; }

        public class Entry
        {
            public string Name;
            public string Phone;
            public string Type; 
        }

        public PhoneBook(string filename)
        {
            string currDir = Directory.GetCurrentDirectory();
            this.filepath = Path.Combine(currDir, filename);

            if (!File.Exists(this.filepath))
            {
                // create a XML tree 
                XDocument xmlDocument = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    // root element
                    new XElement("PhoneBook", null));

                xmlDocument.Save(this.filepath);
            }
        }

        public Entry GetByName(string name)
        {
            Entry result = null;
            XDocument xmlDoc = XDocument.Load(this.filepath);

            //search for an Entry with that name    
            XElement elementToSearch = xmlDoc.Descendants("Entry").Where(x => x.Element("Name").Value == name).FirstOrDefault();

            if (elementToSearch != null)
            {
                result = new Entry
                {
                    Name = elementToSearch.Element("Name").Value,
                    Type = elementToSearch.Element("Type").Value,
                    Phone = elementToSearch.Element("Phone").Value
                };
            }

            return result;
        }

        public void InsertOrUpdate(Entry e)
        {

            XDocument xmlDoc = XDocument.Load(this.filepath);

            XElement elementToAdd = new XElement("Entry",
            new XElement("Name", e.Name),
            new XElement("Phone", e.Phone),
            new XElement("Type", e.Type)
            );

            // first check if there is an Entry with the same name 
            XElement entryToUpdate = xmlDoc.Descendants("Entry").Where(x => x.Element("Name").Value == e.Name).FirstOrDefault();
       
            if (entryToUpdate == null)
            {
                xmlDoc.Element("PhoneBook").AddFirst(elementToAdd);
            }
            else
            {
                entryToUpdate.ReplaceWith(elementToAdd);
            }

            xmlDoc.Save(this.filepath);
        }

        public IEnumerable<Entry> Iterate()
        {
            XDocument xmlDoc = XDocument.Load(this.filepath);

            // Descendants returns IEnumerable<XElement> so we need to create each compatible Object Entry 
            IEnumerable<Entry> contacts = xmlDoc.Descendants("Entry")
                                          .Select(x =>
            new Entry
            {
                Name = x.Element("Name").Value,
                Phone = x.Element("Phone").Value,
                Type = x.Element("Type").Value
            }).ToList().OrderBy(x => x.Name);

            return contacts;
        }
    }

}