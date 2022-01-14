using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using TMPro;
using System.Linq;
using System.Collections;

public class LeaderboardTableManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform table;
    public TextMeshProUGUI playersCount;
    public TableRow mainRow;
    public Scrollbar scrollbar;

    private List<TableRow> rows = new List<TableRow>();
    private DownloadScoresRequest downloadScoresRequest;
    private LeaderboardRange range;
    private LeaderboardEntry userEntry = null;
    private Button button;
    private SteamLeaderboard leaderboard = null;

    public void ShowLeaderboard()
    {
        button = mainRow.GetComponent<Button>();

        if (!SteamManager.Initialized)
            return;

        if (leaderboard == null)
            SteamLeaderboard.Find(Leaderboards.TIMER, OnLeaderboardFound);
    }

    public void OnLeaderboardFound(SteamLeaderboard steamLeaderboard)
    {
        var rowsCount = SteamUserStats.GetLeaderboardEntryCount(leaderboard = steamLeaderboard);

        if (rowsCount > 0)
        {
            leaderboard.DownloadScores(
                ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser,
                0, 0, new int[1], OnUserScoresDownloaded
            );
        }

        playersCount.text = $"{rowsCount} {playersCount.GetComponent<LocalizedText>().Text}";
        playersCount.gameObject.SetActive(true);
    }

    public void OnUserScoresDownloaded(LeaderboardEntry[] leaderboardEntries)
    {
        if (button.interactable = leaderboardEntries.Any())
            userEntry = leaderboardEntries.First();

        Debug.Log($"playerExists: {userEntry != null}");

        downloadScoresRequest = new DownloadScoresRequest();
        range = new LeaderboardRange(downloadScoresRequest.Method);

        leaderboard.DownloadScores(downloadScoresRequest.Method, range.start, range.end, new int[1], OnScoresDownloaded);
    }

    public void OnScoresDownloaded(LeaderboardEntry[] leaderboardEntries)
    {
        if (!leaderboardEntries.Any())
            return;
        
        mainRow.gameObject.SetActive(true);

        var entriesList = leaderboardEntries.ToList();

        //entriesList.AddRange(new LeaderboardEntry[] //testing only
        //{
        //    new LeaderboardEntry((CSteamID)76561198281308457, 1, (int)new TimeSpan(0, 0, 4, 33, 13).TotalMilliseconds, new int[] {41}),
        //    new LeaderboardEntry((CSteamID)76561198997294637, 2, (int)new TimeSpan(0, 0, 8, 4, 12).TotalMilliseconds, new int[] {66}),
        //    new LeaderboardEntry((CSteamID)76561198020891050, 3, (int)new TimeSpan(0, 0, 9, 35, 164).TotalMilliseconds, new int[] {72}),
        //    new LeaderboardEntry((CSteamID)76561198852541823, 4, (int)new TimeSpan(0, 0, 10, 11, 434).TotalMilliseconds, new int[] {82}),
        //    new LeaderboardEntry((CSteamID)76561198419904971, 5, (int)new TimeSpan(0, 0, 12, 52, 66).TotalMilliseconds, new int[] {123}),
        //    new LeaderboardEntry((CSteamID)76561198136138578, 6, (int)new TimeSpan(0, 0, 13, 23, 16).TotalMilliseconds, new int[] {150}),
        //    new LeaderboardEntry((CSteamID)76561198166122702, 7, (int)new TimeSpan(0, 0, 13, 53, 67).TotalMilliseconds, new int[] {155}),
        //    new LeaderboardEntry((CSteamID)76561199069886541, 8, (int)new TimeSpan(0, 0, 24, 35, 17).TotalMilliseconds, new int[] {302}),
        //    new LeaderboardEntry((CSteamID)76561198800952088, 9, (int)new TimeSpan(0, 0, 25, 55, 280).TotalMilliseconds, new int[] {313}),
        //    new LeaderboardEntry((CSteamID)76561198873666063, 10, (int)new TimeSpan(0, 1, 31, 11, 145).TotalMilliseconds, new int[] {1510}),
        //    //new LeaderboardEntry(SteamUser.GetSteamID(), 11, (int)new TimeSpan(1, 2, 1, 23, 43).TotalMilliseconds, new int[] {45}),
        //    new LeaderboardEntry((CSteamID)76561198997294637, 12, (int)new TimeSpan(0, 0, 8, 4, 12).TotalMilliseconds, new int[] {66}),
        //    new LeaderboardEntry((CSteamID)76561198020891050, 13, (int)new TimeSpan(0, 0, 9, 35, 164).TotalMilliseconds, new int[] {72}),
        //    new LeaderboardEntry((CSteamID)76561198852541823, 14, (int)new TimeSpan(0, 0, 10, 11, 434).TotalMilliseconds, new int[] {82}),
        //    new LeaderboardEntry((CSteamID)76561198419904971, 15, (int)new TimeSpan(0, 0, 12, 52, 66).TotalMilliseconds, new int[] {123}),
        //    new LeaderboardEntry((CSteamID)76561198136138578, 16, (int)new TimeSpan(0, 0, 13, 23, 16).TotalMilliseconds, new int[] {150}),
        //    new LeaderboardEntry((CSteamID)76561198166122702, 17, (int)new TimeSpan(0, 0, 13, 53, 67).TotalMilliseconds, new int[] {155}),
        //    new LeaderboardEntry((CSteamID)76561199069886541, 18, (int)new TimeSpan(0, 0, 24, 35, 17).TotalMilliseconds, new int[] {302}),
        //    new LeaderboardEntry((CSteamID)76561198800952088, 19, (int)new TimeSpan(0, 0, 25, 55, 280).TotalMilliseconds, new int[] {313}),
        //    new LeaderboardEntry((CSteamID)76561198873666063, 20, (int)new TimeSpan(0, 1, 31, 11, 145).TotalMilliseconds, new int[] {1510}),
        //    new LeaderboardEntry((CSteamID)76561198281308457, 21, (int)new TimeSpan(1, 2, 1, 23, 43).TotalMilliseconds, new int[] {2001})
        //});

        foreach (var entry in entriesList)
        {
            TableRow rowComponent = Instantiate(rowPrefab, table).GetComponent<TableRow>();
            rowComponent.SetData(entry);

            rows.Add(rowComponent);
        }

        TableRow highLightedRow = null;

        switch (downloadScoresRequest.Method)
        {
            case ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal:
                highLightedRow = rows.First();

                mainRow.SetData(userEntry ?? highLightedRow.Entry);

                ScrollToTop(.25f);
                break;

            case ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser:
                highLightedRow = rows.Find(r => r.Entry.SteamID == SteamUser.GetSteamID());

                leaderboard.DownloadScores(
                    ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal,
                    1, 1, new int[1], entries => mainRow.SetData(entries.First())
                );
                break;
        }

        highLightedRow?.Highlight();
    }

    public void ScrollToTop(float delay)
    {
        StartCoroutine(ScrollTable(delay));
    }

    public IEnumerator ScrollTable(float delay)
    {
        yield return new WaitForSeconds(delay);

        scrollbar.value = 1;
    }

    public void OnMainRowClicked()
    {
        if (userEntry == null)
            return;

        mainRow.gameObject.SetActive(false);

        foreach (var row in rows)
            Destroy(row.gameObject);

        rows = new List<TableRow>();

        downloadScoresRequest.ChangeMethod();
        range.Update(downloadScoresRequest.Method);
        leaderboard.DownloadScores(downloadScoresRequest.Method, range.start, range.end, new int[1], OnScoresDownloaded);
    }
}

public class LeaderboardRange
{
    public int start, end;

    public LeaderboardRange(ELeaderboardDataRequest method)
    {
        Update(method);
    }

    public void Update(ELeaderboardDataRequest method)
    {
        switch (method)
        {
            case ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal:
                start = 1;
                end = 21;
                break;
            case ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser:
                start = -10;
                end = 10;
                break;
        }
    }
}

public class DownloadScoresRequest
{
    public ELeaderboardDataRequest Method { get; private set; }

    public DownloadScoresRequest()
    {
        Method = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal;
    }

    public void ChangeMethod()
    {
        Method = (Method == ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser) 
            ? ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal 
            : ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser;
    }
}