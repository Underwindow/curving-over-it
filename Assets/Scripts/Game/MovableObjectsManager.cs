using UnityEngine;

public class MovableObjectsManager : MonoBehaviour, IPreferenceLoader
{
    [SerializeField] private Rotator[] rotators;
    [SerializeField] private Platform[] platforms;

    private void FixedUpdate()
    {
        foreach (var rotator in rotators)
            rotator.FixedUpdate();

        foreach (var platform in platforms)
            platform.FixedUpdate();
    }

    public void InitData()
    {
        for (int i = 0; i < rotators.Length; i++)
        {
            if (rotators[i].storable)
                rotators[i].Rotation = MyPlayerPrefs.GetData<RotatorsData>(i).rotation;
        }

        for (int i = 0; i < platforms.Length; i++)
            platforms[i].Init(i);
    }

    public void SaveData()
    {
        for (int i = 0; i < rotators.Length; i++)
        {
            if (rotators[i].storable)
                MyPlayerPrefs.Save(new RotatorsData(rotators[i]), i);
        }

        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].storable)
                MyPlayerPrefs.Save(new PlatformsData(platforms[i]), i);
        }
    }

    public void RemoveData()
    {
        for (int i = 0; i < rotators.Length; i++)
            MyPlayerPrefs.Remove<RotatorsData>(i);

        for (int i = 0; i < platforms.Length; i++)
            MyPlayerPrefs.Remove<PlatformsData>(i);
    }
}
