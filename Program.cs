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
      Console.WriteLine("Enter the file name you'd like to convert:");

      string xmlFilePath;
      string jsonFileName;
      string xmlFileName = Console.ReadLine();
      while (xmlFileName.Length < 1) {
        Console.WriteLine("Enter the file name you'd like to convert:");
        xmlFileName = Console.ReadLine();
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
      var bookElements = xmlDocument.SelectNodes("//book");
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