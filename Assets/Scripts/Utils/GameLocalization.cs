using UnityEngine;
using Steamworks;

public class GameLocalization : MonoBehaviour
{
    public UserLanguage Language { get; private set; }

    public static GameLocalization Instance { get; set; }

    private void Awake()
    {
        CreateSingleton();
        SetUserLanguage();
    }

    private void CreateSingleton()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void SetUserLanguage()
    {
        try {
            if (SteamManager.Initialized) {
                Debug.Log("SteamApps.CurrentGameLanguage: " + SteamApps.GetCurrentGameLanguage());
                switch (SteamApps.GetCurrentGameLanguage()) {
                    case "russian":
                        Language = UserLanguage.rus;
                        break;
                    default:
                        Language = UserLanguage.eng;
                        break;
                }
            }
        }
        catch {
            Debug.LogError($"Steam is not Initialized");
        }
    }
}

public enum UserLanguage
{
    eng,
    rus
}
