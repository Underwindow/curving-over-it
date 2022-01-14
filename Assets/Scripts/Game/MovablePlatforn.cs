using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovablePlatforn : MonoBehaviour
{
    [SerializeField] Rigidbody2D platform;
    [SerializeField] float movingTime;
    [SerializeField] Transform[] positions;

    private List<Tuple<Vector2, Quaternion>> positionsList = new List<Tuple<Vector2, Quaternion>>();
    private int posIndex;
    private float t = 0;
    private Tuple<Vector2, Quaternion> destination, startPos;
    private Vector2 velocity = Vector2.zero;

    private void Start()
    {
        positionsList.AddRange(
            positions.Select(p => Tuple.Create((Vector2)p.position, p.rotation))
        );

        startPos = positionsList[0];
        destination = positionsList[posIndex = 1];
    }

    private void FixedUpdate()
    {
        if ((platform.position - destination.Item1).magnitude > .1f)
        {
            t += Time.fixedDeltaTime / movingTime;
            platform.MovePosition(Vector2.SmoothDamp(platform.position, destination.Item1, ref velocity, movingTime));
            platform.MoveRotation(Quaternion.Slerp(startPos.Item2, destination.Item2, t));
        }
        else
        {
            t = 0;
            ChangeDestination();
        }
    }
    public void ChangeDestination()
    {
        posIndex++;

        if (!(positionsList.Count - posIndex > 0)) {
            positionsList.Reverse();
            posIndex = 1;
        }

        startPos = positionsList[posIndex - 1];
        destination = positionsList[posIndex];
    }
}
