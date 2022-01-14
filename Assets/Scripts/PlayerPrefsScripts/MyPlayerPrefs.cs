using System;

public abstract class MyPlayerPrefs
{
    public static void Save<T>(T data, int id = 0) where T : IPreferenceData
    {
        var serialized = SerializationExtensions.XMLSerialize(data);
        Storage.PutString(data.Key + id, serialized);
    }

    public static bool Exists<T>(int id = 0) where T : IPreferenceData, new()
        => Exists(new T(), id);

    public static bool Exists<T>(T data, int id = 0) where T : IPreferenceData, new()
        => Storage.DataExists(data.Key + id);

    public static void Remove<T>(int id = 0) where T : IPreferenceData, new()
        => Storage.RemoveData(new T().Key + id);

    public static T GetData<T>(int id = 0) where T : IPreferenceData, new()
    {
        var data = new T();
        var key = data.Key + id;

        if (Exists(data, id)) {
            var xmlData = Storage.GetString(key, null);
            
            if (xmlData != null)
                data = SerializationExtensions.XMLDeserialize<T>(xmlData);
            else
                throw new NullReferenceException($"No data by key: {key}");
        }

        return data;
    }
}