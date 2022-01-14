using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour, IPreferenceLoader, IFinishEventListener
{
    public static Timer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    [SerializeField]
    private TextMeshProUGUI timer;
    private GameObject parentObj;

    private void Start()
    {
        parentObj = timer.transform.parent.gameObject;
        timer.text = "";
    }

    public SafeFloat SavedValue { get; private set; }
    public SafeFloat Value => SavedValue;

    private void Update()
    {
        SavedValue += Time.deltaTime;
    }

    private void OnGUI()
    {
        if (timer == null)
            return; 

        var elapsedTime = TimeSpan.FromSeconds((float)Value);
        var fmtString = @"ss\.fff";

        if (elapsedTime.Minutes > 0 || elapsedTime.Hours > 0)
            fmtString = fmtString.Insert(0, @"mm\:");
        if (elapsedTime.Hours > 0 || elapsedTime.Days > 0)
            fmtString = fmtString.Insert(0, @"hh\:");
        if (elapsedTime.Days > 0)
            fmtString = fmtString.Insert(0, @"d\:");

        timer.text = elapsedTime.ToString(fmtString);
    }

    public void InitData()
    {
        if (MyPlayerPrefs.Exists<TimerData>())
            SavedValue = MyPlayerPrefs.GetData<TimerData>().timerValue;
    }

    public void SaveData()
    {
        MyPlayerPrefs.Save(new TimerData(this));
    }

    public void RemoveData()
    {
        MyPlayerPrefs.Remove<TimerData>();
    }

    public void OnPlayerFinished(object sender, PlayerFinishedEventArgs args)
    {
        parentObj.SetActive(false);
    }
}