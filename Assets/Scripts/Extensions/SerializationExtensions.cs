using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class SerializationExtensions
{
    public static string XMLSerialize<T>(T dataToSerialize)
    {
        try
        {
            var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stringwriter, dataToSerialize);
            return stringwriter.ToString();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to serialize. Reason: " + e.Message);
            throw e;
        }
    }

    public static byte[] BinarySerialize<T>(T dataToSerialize)
    {
        try
        {
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, dataToSerialize);
                bytes = stream.ToArray();
            }

            return bytes;
        }
        catch (SerializationException e)
        {
            Debug.LogError("Failed to deserialize. Reason: " + e.Message);
            throw;
        }
    }

    public static T BinaryDeserialize<T>(byte[] buffer)
    {
        T res;

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                res = (T)formatter.Deserialize(stream);
            }
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to deserialize. Reason: " + e.Message);
            throw;
        }

        return res;
    }

    public static T XMLDeserialize<T>(string xmlText)
    {
        try
        {
            var stringReader = new StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stringReader);
        }
        catch (Exception e)
        {
            Debug.Log("Failed to deserialize. Reason: " + e.Message);
            throw e;
        }
    }

    public static XElement DictToXml(Dictionary<string, string> dict)
    {
        return  
            new XElement("items",
                dict.Select(x => new XElement("item", new XAttribute("key", x.Key), new XAttribute("value", x.Value)))
            );
    }

    public static Dictionary<string, string> JsonToDict(string json)
    {
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
    }

    public static string DictToJson(Dictionary<string, string> dict)
    {
        return JsonConvert.SerializeObject(dict);
    }

    public static Dictionary<string, string> XmlToDict(string xml)
    {
        XElement xElem = XElement.Parse(xml);
        
        var dict = xElem
            .Descendants("item")
            .ToDictionary(x => (string)x.Attribute("key"), x => (string)x.Attribute("value"));

        return dict;
    }
}
