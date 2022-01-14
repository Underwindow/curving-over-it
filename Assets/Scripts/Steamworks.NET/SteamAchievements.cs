using System;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System.Linq;

public class SteamAchievements : MonoBehaviour
{
    public const string 
        SICK      = "SACH_SICK",
        AMATEUR   = "SACH_AMATEUR",
        VELIAL    = "SACH_VELIAL",
        CRAZY     = "SACH_CRAZY",
        GENIUS    = "SACH_GENIUS",
        EXHAUSTED = "SACH_EXHAUSTED",
        FIRST     = "SACH_FIRST",
        GLITCHER  = "SACH_GLITCHER",
        MAGNUS    = "SACH_MAGNUS";

    public readonly List<IAchievement> achievements = new List<IAchievement>();

    public static SteamAchievements Instance { get; set; }

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        if (!SteamManager.Initialized)
            return;

        if (SteamUserStats.RequestCurrentStats())
        {
            if (!achievements.Any())
            {
                Callback<UserStatsReceived_t>.Create((cb) => {
                    if (cb.m_eResult != EResult.k_EResultFail)
                    {
                        Debug.Log($"SteamUserStats.RequestCurrentStats result success");
                        InitAchievements();
                    }
                    else
                        Debug.LogError($"SteamUserStats.RequestCurrentStats result failure: {cb.m_eResult}");
                });
            }
        }
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitAchievements()
    {
        achievements.AddRange(
            new IAchievement[] {
                new TimeAchievement(FIRST, TimeSpan.MaxValue),
                new TimeAchievement(SICK, new TimeSpan(0, 3, 0)),
                new TimeAchievement(AMATEUR, new TimeSpan(1, 0, 0)),
                new TimeAchievement(VELIAL, new TimeSpan(0, 30, 0)),
                new TimeAchievement(CRAZY, new TimeSpan(0, 10, 0)),
                new TimeAchievement(GENIUS, new TimeSpan(0, 5, 0)),
                new TimeAchievement(EXHAUSTED, new TimeSpan(0, 15, 0)),
                new CollisionAchievement(GLITCHER),
                new CollisionAchievement(MAGNUS)
            });
    }
}

public class TimeAchievement : SteamAchievement
{
    public TimeSpan TimeToUnlock { get; private set; }

    public TimeAchievement(string name, TimeSpan time) : base(name)
    {
        TimeToUnlock = time;
    }
}

public class CollisionAchievement : SteamAchievement
{
    public Collider2D[] Colliders;
    public CollisionAchievement(string name) : base(name) {}
}

public class SteamAchievement : IAchievement
{
    public bool unlocked = false;
    public string Name { get; }

    public bool IsUnlocked => unlocked;

    public SteamAchievement(string name)
    {
        Name = name;

        if (SteamUserStats.GetAchievement(Name, out unlocked))
            Debug.Log($"GetAchievement({Name}, '{unlocked}'): success");
        else
            Debug.LogError($"GetAchievement({Name}): failure");
    }


    public void Unlock()
    {
        if (unlocked)
            return;

        if (SteamUserStats.SetAchievement(Name))
            Debug.Log($"SetAchievement({Name}): success");
        else
            Debug.LogError($"SetAchievement({Name}): failure");

        if (SteamUserStats.StoreStats())
            Debug.Log($"StoreStats({Name}): success; Unlocked: {unlocked = true}");
        else
            Debug.LogError($"StoreStats({Name}): failure");
    }
}

public interface IAchievement
{
    string Name { get; }
    bool IsUnlocked { get; }
    void Unlock();
}
