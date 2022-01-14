using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Platform
{
    [SerializeField] private Rigidbody2D platform;
    [SerializeField] private float movingTime;
    [SerializeField] private Transform[] positions;
    [SerializeField] public bool storable;

    public Vector2 Position {
        get => platform.position;
        private set { platform.position = value; }
    }
    public Quaternion Rotation
    {
        get => platform.transform.rotation;
        private set { platform.transform.rotation = value; }
    }

    public Transform Destination { get; private set; }
    public Transform StartPos { get; private set; }
    public int PosIndex { get; private set; }
    public float SlerpValue { get; private set; } = 0;
    private float SmoothValue => Mathf.Pow(Mathf.Sin(SlerpValue * Mathf.PI * .5f), 2);

    private List<Transform> positionsList = new List<Transform>();

    public void Init(int platformID)
    {
        foreach (var pos in positions) {
            var obj = new GameObject();
            obj.transform.position = pos.position;
            obj.transform.rotation = pos.rotation;
            positionsList.Add(obj.transform);
        }

        StartPos = positionsList[0];
        Destination = positionsList[PosIndex = 1];

        if (storable && MyPlayerPrefs.Exists<PlatformsData>(platformID))
        {
            var data = MyPlayerPrefs.GetData<PlatformsData>(platformID);

            Position = data.position;
            Rotation = data.rotation;
            StartPos.position = data.startPos;
            StartPos.rotation = data.startRot;
            Destination.position = data.destPos;
            Destination.rotation = data.destRot;
            SlerpValue = data.slerpVal;
            PosIndex = data.posIndex;

            platform.transform.position = data.position;
            platform.MoveRotation(data.rotation);
        }
    }

    public void FixedUpdate()
    {
        if (SlerpValue <= 1f)
            Move();
        else
            ChangeDestination();
    }

    private void Move()
    {
        SlerpValue += Time.fixedDeltaTime / movingTime;
        platform.MovePosition(Vector3.Slerp(StartPos.position, Destination.position, SmoothValue));
        platform.MoveRotation(Vector3.Slerp(StartPos.rotation.eulerAngles, Destination.rotation.eulerAngles, SmoothValue));
    }

    private void ChangeDestination()
    {
        SlerpValue = 0;
        PosIndex++;

        if (!(positionsList.Count - PosIndex > 0))
        {
            positionsList.Reverse();
            PosIndex = 1;
        }

        StartPos = positionsList[PosIndex - 1];
        Destination = positionsList[PosIndex];
    }
}
