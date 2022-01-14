using Steamworks;
using UnityEngine;

public class SteamLeaderboard
{
    public string Name { get; private set; }
    public SteamLeaderboard_t Leaderboard { get; private set; }

    public SteamLeaderboard(string leaderboardName, SteamLeaderboard_t leaderboard)
    {
        Name = leaderboardName;
        Leaderboard = leaderboard;
    }

    public static implicit operator SteamLeaderboard_t(SteamLeaderboard instance) 
        => instance.Leaderboard;

    public delegate void FindSuccessHandler(SteamLeaderboard leaderboard);

    public static void Find(string pchLeaderboardName, FindSuccessHandler onSuccessFunc = null)
    {
        var m_callResultFindLeaderboard = CallResult<LeaderboardFindResult_t>
            .Create((result, failure) => {
                if (result.m_bLeaderboardFound != 1 || failure)
                    Debug.LogError($"Leaderboard {pchLeaderboardName} could not be found");
                else
                {
                    Debug.Log($"Leaderboard {pchLeaderboardName} found");
                    
                    var foundLeaderboard = new SteamLeaderboard(pchLeaderboardName, result.m_hSteamLeaderboard);
                    onSuccessFunc?.Invoke(foundLeaderboard);
                }
            });

        SteamAPICall_t steamAPICall = SteamUserStats.FindLeaderboard(pchLeaderboardName);
        m_callResultFindLeaderboard.Set(steamAPICall);
    }

    public delegate void ScoreUploadedSuccessHandler(SteamLeaderboard leaderboard, int score);

    public void UploadScore(int score, int[] details = null, ScoreUploadedSuccessHandler scoreUploadedSuccessFunc = null)
        => UploadScore(this, score, details, scoreUploadedSuccessFunc);

    public static void UploadScore(SteamLeaderboard leaderboard, int score, int[] details, ScoreUploadedSuccessHandler scoreUploadedSuccessFunc)
    {
        if (leaderboard == null)
            return;

        var m_callResultUploadScore = CallResult<LeaderboardScoreUploaded_t>
            .Create((result, failure) => {
                if (result.m_bSuccess != 1 || failure)
                    Debug.LogError("Score could not be uploaded to Steam\n");
                else
                {
                    Debug.Log($"Score {score} uploaded to Leaderboard {leaderboard.Name}\n");
                    scoreUploadedSuccessFunc?.Invoke(leaderboard, score);
                }
            });

        SteamAPICall_t steamAPICall = SteamUserStats.UploadLeaderboardScore(
            leaderboard, uploadScoreMethod, score, details, details?.Length ?? 0);

        m_callResultUploadScore.Set(steamAPICall);
    }

    public delegate void DownloadScoresSuccessHandler(LeaderboardEntry[] leaderboardEntries);

    public void DownloadScores(
        int inRangeStart = 0, int inRangeEnd = 0, 
        int[] details = null, 
        DownloadScoresSuccessHandler onDownloadScoresSuccess = null)
    {
        DownloadScores(this, downloadScoresMethod, inRangeStart, inRangeEnd, details, onDownloadScoresSuccess);
    }

    public void DownloadScores(
        ELeaderboardDataRequest requestMethod,
        int inRangeStart = 0, int inRangeEnd = 0,
        int[] details = null,
        DownloadScoresSuccessHandler onDownloadScoresSuccess = null)
    { 
        DownloadScores(this, requestMethod, inRangeStart, inRangeEnd, details, onDownloadScoresSuccess); 
    }

    public static void DownloadScores(
        SteamLeaderboard leaderboard, 
        ELeaderboardDataRequest requestMethod, 
        int inRangeStart, int inRangeEnd, int[] details,
        DownloadScoresSuccessHandler onDownloadScoresSuccess)
    {
        if (leaderboard == null)
            return;

        var m_callResultDownloadScores = CallResult<LeaderboardScoresDownloaded_t>.Create(
            (result, failure) => {
                if (failure) {
                    Debug.LogError($"DownloadScores from {leaderboard.Name}: Failure");
                    return;
                }

                var entryCount = result.m_cEntryCount;
                var tempEntries = new LeaderboardEntry_t[entryCount];
                var entries = new LeaderboardEntry[entryCount];
                
                Debug.Log($"DownloadScores from {leaderboard.Name}: Success\nEntries count: {entryCount}");

                for (int index = 0; index < entryCount; index++)
                {
                    var tempDetails = new int[details?.Length ?? 0];

                    SteamUserStats.GetDownloadedLeaderboardEntry(
                        result.m_hSteamLeaderboardEntries, index, out tempEntries[index],
                        tempDetails, tempDetails?.Length ?? 0
                    );

                    entries[index] = new LeaderboardEntry(tempEntries[index], tempDetails);
                }

                onDownloadScoresSuccess?.Invoke(entries);
            });

        SteamAPICall_t steamAPICall = SteamUserStats.DownloadLeaderboardEntries(
            leaderboard, requestMethod, inRangeStart, inRangeEnd);

        m_callResultDownloadScores.Set(steamAPICall);
    }

    //testing
    //private const ELeaderboardUploadScoreMethod uploadScoreMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate;

    private const ELeaderboardUploadScoreMethod uploadScoreMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest;

    private const ELeaderboardDataRequest downloadScoresMethod = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser;
}

public class LeaderboardEntry
{
    public CSteamID SteamID { get; private set; }
    public int Rank { get; private set; }
    public int Score { get; private set; }
    public int[] Details { get; private set; } 
    public UGCHandle_t? UGCHandle { get; private set; }

    public LeaderboardEntry(CSteamID steamID, int rank, int score, int[] details, UGCHandle_t? ugsHandle = null)
    {
        SteamID = steamID;
        Rank = rank;
        Score = score;
        Details = details;
        UGCHandle = ugsHandle;
    }

    public LeaderboardEntry(LeaderboardEntry_t leaderboardEntry, int[] details)
    {
        SteamID = leaderboardEntry.m_steamIDUser;
        Rank = leaderboardEntry.m_nGlobalRank;
        Score = leaderboardEntry.m_nScore;
        UGCHandle = leaderboardEntry.m_hUGC;
        Details = details;
    }
}
