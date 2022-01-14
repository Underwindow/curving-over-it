using UnityEngine;

public class CameraController : MonoBehaviour, IPreferenceLoader
{
    [SerializeField] private Camera[] cameras;
    private Transform player;
    private Vector3 velocity = Vector3.zero;
    private Vector3 destination;
    private Controls controls;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }
    
    private void Update()
    {
        destination = GetDestination();
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, .1f);
        foreach (var camera in cameras)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, UpdateCameraSize(), Time.deltaTime);
        }
    }

    private Vector3 GetDestination()
        => Vector3.Scale(player.position, new Vector3(1, 1, 0)) + Vector3.forward * transform.position.z;

    private float UpdateCameraSize()
    {
        var startSize = 3.5f;

        if (Controls.Instance.IsZoomHold)
            return startSize + 1.5f;
        else
            return 3.5f;
    }

    public void InitData()
    {
        if (MyPlayerPrefs.Exists<MainCameraData>())
            transform.position = MyPlayerPrefs.GetData<MainCameraData>().position;
    }

    public void SaveData()
    {
        MyPlayerPrefs.Save(new MainCameraData(this));
    }

    public void RemoveData()
    {
        MyPlayerPrefs.Remove<MainCameraData>();
    }
}
