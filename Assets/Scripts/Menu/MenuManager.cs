using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    
    [SerializeField] private GameObject loading, endBlackScreen;
    
    private SoundEffect mainTheme;
    private LevelLoader levelLoader;
    private float loadingTime = 3.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        mainTheme = FindObjectOfType<AudioManager>().Play(SoundEffectType.Menu);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R)) //testing only
    //    {
    //        
    //        PlayerPrefs.DeleteAll();
    //        Storage.Clear();
    //        //foreach (var ach in SteamAchievements.Instance.achievements)
    //        //    SteamUserStats.ClearAchievement((ach as SteamAchievement).Name);
    //        //SteamUserStats.StoreStats();
    //    }
    //}

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("PLAYED_ONCE"))
        {
            levelLoader.LoadLevel(1, .5f);
            StartCoroutine(AudioFade.FadeOut(mainTheme, .25f, Mathf.Lerp, .25f));
        }
        else {
            levelLoader.LoadLevel(1, loadingTime);
            PlayerPrefs.SetInt("PLAYED_ONCE", 1);

            StartCoroutine(AudioFade.FadeOut(mainTheme, loadingTime - .5f, Mathf.Lerp, .25f));
        }
    }

    public IEnumerator StopAnimLoading(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        endBlackScreen.SetActive(true);
    }
}
