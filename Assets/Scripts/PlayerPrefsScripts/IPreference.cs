public interface IPreferenceData
{
    string Key { get; }
}

public interface IPreferenceLoader
{
    void InitData();
    void SaveData();
    void RemoveData();
}