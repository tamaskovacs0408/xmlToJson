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


      string xmlFilePath;
      string jsonFileName;
      Console.WriteLine("Enter the file name you'd like to convert: ");
      string xmlFileName = Console.ReadLine().Trim().ToLower();
      while (xmlFileName.Length < 1) {
        Console.Write("Enter the file name you'd like to convert: ");
        xmlFileName = Console.ReadLine().Trim().ToLower();
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
      Console.WriteLine("Enter the section name for you'd like to create the json objects: ");
      string xmlElementName = Console.ReadLine().Trim().ToLower();
      while (xmlElementName.Length < 1) {
        Console.WriteLine("Enter the section name for you'd like to create the json objects: ");
        xmlElementName = Console.ReadLine().Trim().ToLower();
      }

      var xmlElements = xmlDocument.SelectNodes($"//{xmlElementName}");
      JArray jsonArray = new JArray();

      foreach (XmlNode xmlNode in xmlElements)
      {
        string jsonElement = JsonConvert.SerializeXmlNode(xmlNode);
        JObject jsonObject = JObject.Parse(jsonElement);
        jsonArray.Add(jsonObject);
      }

      // Saving json file
      File.WriteAllTextAsync(jsonFilePath, jsonArray.ToString());

      Console.WriteLine("Converting successfully finished!");
    }
    public static void Main(string[] args)
    {
      XmlToJson();
    }
  }
}