using UnityEngine;
using TMPro;

public class HitsCounter : MonoBehaviour, IPreferenceLoader, IFinishEventListener
{
    [SerializeField]
    private TextMeshProUGUI hits;
    public SafeFloat HitsCount { get; private set; }

    private GameObject parentObj;

    private void Start()
    {
        parentObj = hits.transform.parent.gameObject;
    }

    public void IncreaseCountOfHits() {
        if (hits == null)
            return;

        HitsCount += new SafeFloat(1);
        hits.text = HitsCount.ToString();
    }

    public void InitData()
    {
        if (MyPlayerPrefs.Exists<HitsCountData>())
            HitsCount = MyPlayerPrefs.GetData<HitsCountData>().hitsCount;

        hits.text = HitsCount.ToString();
    }

    public void SaveData()
    {
        MyPlayerPrefs.Save(new HitsCountData(this));
    }

    public void RemoveData()
    {
        MyPlayerPrefs.Remove<HitsCountData>();
    }

    public void OnPlayerFinished(object sender, PlayerFinishedEventArgs args)
    {
        parentObj.SetActive(false);
    }
}