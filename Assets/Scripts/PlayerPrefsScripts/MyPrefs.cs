using System;
using UnityEngine;

[Serializable]
public class TimerData : IPreferenceData
{
    public float timerValue;

    public TimerData() : base() { }

    public TimerData(Timer timer)
    {
        timerValue = (float)timer.Value;
    }

    public string Key => "TimerData";
}

[Serializable]
public class PlayerRbData : IPreferenceData
{
    public Vector3 position;
    public Vector2 velocity;
    public float 
        rotation, 
        angularVelocity, 
        angularDrag;

    public PlayerRbData() : base() { }

    public PlayerRbData(Rigidbody2D playerRb)
    {
        position = playerRb.position;
        velocity = playerRb.velocity;
        rotation = playerRb.rotation;
        angularVelocity = playerRb.angularVelocity;
        angularDrag = playerRb.angularDrag;
    }

    public void SetTo(Rigidbody2D playerRb)
    {
        playerRb.position = position;
        playerRb.velocity = velocity;
        playerRb.rotation = rotation;
        playerRb.angularVelocity = angularVelocity;
        playerRb.angularDrag = angularDrag;
    }

    public string Key => "PlayerRbData";
}

[Serializable]
public class RotatorsData : IPreferenceData
{
    public float rotation;

    public RotatorsData() : base() { }
    public RotatorsData(Rotator rotator)
    {
        rotation = rotator.Rotation;
    }

    public string Key => "RotatorsData";
}

[Serializable]
public class PlatformsData : IPreferenceData
{
    public Vector2 position;
    public Quaternion rotation;
    public Vector3 startPos;
    public Quaternion startRot;
    public Vector3 destPos;
    public Quaternion destRot;
    public float slerpVal;
    public int posIndex;

    public PlatformsData() : base() { }
    public PlatformsData(Platform platform)
    {
        position = platform.Position;
        rotation = platform.Rotation;
        startPos = platform.StartPos.position;
        startRot = platform.StartPos.rotation;
        destPos = platform.Destination.position;
        destRot = platform.Destination.rotation;
        slerpVal = platform.SlerpValue;
        posIndex = platform.PosIndex;
    }
    public string Key => "PlatformsData";
}

public class MainCameraData : IPreferenceData
{
    public Vector3 position;
    public MainCameraData() : base() { }
    public MainCameraData(CameraController cameraController)
    {
        position = cameraController.transform.position;
    }

    public string Key => "MainCameraData";
}

public class HitsCountData : IPreferenceData
{
    public uint hitsCount;

    public HitsCountData() : base() { }
    public HitsCountData(HitsCounter hitsCounter)
    {
        hitsCount = (uint)hitsCounter.HitsCount;
    }

    public string Key => "HitsCountData";
}

public class SubtitleData : IPreferenceData
{
    public bool played;

    public SubtitleData() : base() { }
    public SubtitleData(SubtitleTrigger subtitle)
    {
        played = subtitle.triggered;
    }

    public string Key => "SubtitleData";
}