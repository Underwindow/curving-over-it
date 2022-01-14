using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Storage
{
    private static Dictionary<string, string> keyValueData = new Dictionary<string, string>();

    public static void Init()
    {
        var path = GetPath();

        if (File.Exists(path))
        {
            Debug.Log("File.Exists");

            var encrypted = File.ReadAllText(path);
            try
            {
                var decryptedJsonStr = EncryptionHelper.Decrypt(encrypted);
                keyValueData = SerializationExtensions.XmlToDict(decryptedJsonStr);
            }
            catch
            {
                keyValueData = new Dictionary<string, string>();
            }
        }

        Debug.Log("Storage.Init()");
    }

    private static string GetPath()
    {
        return RootDir + @"\" + "playerPrefs" + ".pprefs";
    }

    public static readonly string RootDir = 
        AppDataLocalLow.Path() + @"\" + 
        Application.companyName + @"\" + 
        Application.productName;

    public static void PutString(string id, string data)
    {
        keyValueData[id] = data;
    }

    public static string GetString(string id, string defaultValue = null)
    {
        if (keyValueData.ContainsKey(id))
            return keyValueData[id];
        else 
            return defaultValue;
    }


    public static bool DataExists(string id)
    {
        return keyValueData.ContainsKey(id);
    }

    public static void RemoveData(string id)
    {
        if (DataExists(id))
            keyValueData.Remove(id);
    }

    public static void Clear()
    {
        var path = GetPath();
        if (File.Exists(path))
            File.Delete(path);

        keyValueData = new Dictionary<string, string>();
    }

    public static void Save()
    {
        var path = GetPath();
        var jsonStr = SerializationExtensions.DictToXml(keyValueData).ToString();
        var encrypted = EncryptionHelper.Encrypt(jsonStr);

        using (FileStream fs = File.Create(path))
        {
            byte[] info = new UTF8Encoding(true).GetBytes(encrypted);
            
            fs.Write(info, 0, info.Length);
        }
    }
}
