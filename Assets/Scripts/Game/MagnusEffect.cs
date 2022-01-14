using System;
using UnityEngine;

public enum ClockwiseRotation
{
    Positive,
    Negative,
    None
}

public class MagnusEffect : MonoBehaviour
{
    private SafeFloat density, transverseArea, lastRotation;
    private Rigidbody2D rb;
    public float SignedDeltaRotation => rb.rotation - lastRotation.GetValue();

    public ClockwiseRotation Clockwise { 
        get {
            switch (Math.Sign(SignedDeltaRotation)) {
                case -1: return ClockwiseRotation.Positive;
                case 1: return ClockwiseRotation.Negative;
                default: return ClockwiseRotation.None;
            }
        }
    } 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastRotation = rb.rotation;
        transverseArea = Mathf.PI * transform.localScale.x;
        density = 1.2754f;
    }

    private void FixedUpdate()
    {
        if (SignedDeltaRotation != 0) {
            rb.AddMagnusForce(density.GetValue(), SignedDeltaRotation, transverseArea.GetValue(), ForceMode2D.Force);
        }

        lastRotation = rb.rotation;
    }
}
