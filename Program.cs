using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Xml;

namespace XMLtoJSONConverter
{
  class Program
  {
    public static void XmlToJson()
    {
      Console.Write("Enter the file name you'd like to convert: ");

      string xmlFilePath;
      string jsonFileName;
      // string modifiedXmlFileName = null;
      string xmlFileName = Console.ReadLine();
      while (xmlFileName.Length < 1) {
        Console.WriteLine("Enter the file name you'd like to convert: ");
        xmlFileName = Console.ReadLine();
      }
      if (xmlFileName != null) {
        xmlFileName = xmlFileName.Trim().ToLower();
      }

      xmlFilePath = $"./inputs/{xmlFileName}.xml";
      jsonFileName = xmlFileName;

      string jsonFilePath = $"./outputs/{jsonFileName}.json";
      // Reading XML file
      string xml = File.ReadAllText(xmlFilePath);
      // Processing XML and crearing JSON file
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(xml);

      // Get the book elements from the XML and convert them to the json file
      string xmlElementName = Console.ReadLine();
      while (xmlElementName.Length < 1) {
        Console.Write("Enter the section name for you'd like to create the json objects: ");
        xmlElementName = Console.ReadLine();
      }
      if (xmlElementName != null) {
        xmlElementName = xmlElementName.Trim().ToLower();
      }
      var bookElements = xmlDocument.SelectNodes($"//{xmlElementName}");
      JArray booksArray = new JArray();

      foreach (XmlNode bookNode in bookElements)
      {
        string bookJson = JsonConvert.SerializeXmlNode(bookNode);
        JObject bookObject = JObject.Parse(bookJson);
        booksArray.Add(bookObject);
      }

      // Saving json file
      File.WriteAllTextAsync(jsonFilePath, booksArray.ToString());

      Console.WriteLine("Converting successfully finished!");
    }
    public static void Main(string[] args)
    {
      XmlToJson();
    }
  }
}