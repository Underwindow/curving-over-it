using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour, IFinishEventListener
{
    public static GameManager Instance { get; private set; }
    public UnityEvent OnGameAwake;
    
    private List<IPreferenceLoader> prefsList;
    private LevelLoader levelLoader;
    private bool playerFinished;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        prefsList = FindObjectsOfType<MonoBehaviour>().OfType<IPreferenceLoader>().ToList();
        levelLoader = FindObjectOfType<LevelLoader>();
        
        OnGameAwake?.Invoke();
    }

    private void Start()
    {
        prefsList.ForEach(p => p.InitData());
        playerFinished = false;
    }

    public void LoadMenu()
    {
        SaveGame();

        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        prefsList
            .Where(p => !(p is SubtitlesManager)).ToList()
            .ForEach(p => p.RemoveData());

        prefsList.Find(p => p is SubtitlesManager).SaveData();

        levelLoader?.LoadLevel(1);
    }

    private void OnApplicationQuit()
    {
        if (playerFinished)
            return;

        SaveGame();
    }

    public void OnPlayerFinished(object sender, PlayerFinishedEventArgs args)
    {
        playerFinished = true;
        
        try {
            SteamLeaderboard.Find(
                Leaderboards.TIMER,
                leaderboard => {
                    leaderboard.UploadScore(
                        args.PlayerScore,
                        new[] { (int)args.TotalHits },
                        (lbd, score) => Debug.Log($"Player score({score}) was successfully uploaded to {lbd.Name} leaderboard")
                    );
                });
        }
        catch (InvalidOperationException e) {
            Debug.LogError($"{e.Message} Leaderboard UploadScore Failed");
        }

        prefsList
            .Where(p => !(p is SubtitlesManager)).ToList()
            .ForEach(p => p.RemoveData());

        prefsList = prefsList.Where(p => p is SubtitlesManager).ToList();
        prefsList.Find(p => p is SubtitlesManager).SaveData();
        
        Task.Run(() => Storage.Save()).GetAwaiter().GetResult();
    }

    public void OnCaptionsPlayed(object sender, EventArgs args)
    {
        levelLoader?.LoadLevel(0, 1);
    }

    public void SaveData()
    {
        prefsList.ForEach(p => p.SaveData());
    }

    private FixedSizeStack<Action> actions = new FixedSizeStack<Action>(1);

    public void SaveGame()
    {
        SaveData();

        var saveAction = new Action(() => {
            Debug.Log("Saving"); Task.WaitAll(Task.Run(() => Storage.Save())); Debug.Log("Saved");
        });

        try
        {
            actions.Push(saveAction);

            Task.Run(saveAction).GetAwaiter().OnCompleted(() =>
            {
                actions.Pop();
                Debug.Log($"actions.Pop();");
            });
        }
        catch (StackOverflowException)
        {
            Debug.LogError($"Already saving data!");
        }
    }
}
