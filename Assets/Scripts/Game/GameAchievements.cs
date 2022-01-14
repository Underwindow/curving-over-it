using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameAchievements : MonoBehaviour, IFinishEventListener
{
    private List<TimeAchievement> timeAchievements;
    private CollisionAchievement collisionAchievement;
    private List<CollisionAchievement> collisionAchievements;
    private TriggerAchievement[] triggers;

    void Start()
    {
        if (!SteamAchievements.Instance.achievements.Any())
            return;

        collisionAchievements = SteamAchievements.Instance.achievements.OfType<CollisionAchievement>().ToList();
        
        if (collisionAchievements.Any())
        {
            triggers = GetComponentsInChildren<TriggerAchievement>();

            var glitcher = collisionAchievements.Find(ach => ach.Name == SteamAchievements.GLITCHER);

            if (!glitcher.IsUnlocked)
            {
                var tempTriggers = triggers.ToList().Where(tr => tr.collisionAchievementName == SteamAchievements.GLITCHER);
                foreach (var tr in tempTriggers)
                    tr.OnTriggerEnter.AddListener(glitcher.Unlock);
                foreach (var tr in tempTriggers)
                    tr.OnTriggerEnter.AddListener(GameManager.Instance.Restart);
            }

            var magnus = collisionAchievements.Find(ach => ach.Name == SteamAchievements.MAGNUS);

            if (!magnus.IsUnlocked)
            {
                var tempTriggers = triggers.ToList().Where(tr => tr.collisionAchievementName == SteamAchievements.MAGNUS);
                foreach (var tr in tempTriggers)
                    tr.OnTriggerEnter.AddListener(magnus.Unlock);
            }
        }

        timeAchievements = SteamAchievements.Instance.achievements.OfType<TimeAchievement>()?.ToList();
    }

    public void OnPlayerFinished(object sender, PlayerFinishedEventArgs args)
    {
        if (timeAchievements == null || !timeAchievements.Any())
            return;

        if (!timeAchievements.Any(ach => !ach.unlocked)) //if all timeAchievements already unlocked
        {
            Debug.Log("TimeAchievements are already unlocked");
            return;
        }

        TimeSpan time = TimeSpan.FromSeconds(args.FinishTime);

        Debug.Log("Unlocking Time Achievements");

        timeAchievements
            .ForEach(a => Debug.Log($"{a.Name} unlocked? {a.unlocked}, timeToUnlock: {a.TimeToUnlock}, finishTime: {time}, unlock? {a.TimeToUnlock >= time}"));

        timeAchievements
            .Where(a => !a.unlocked && a.TimeToUnlock >= time)
            .ToList().ForEach(achievement => achievement.Unlock());
    }
}
