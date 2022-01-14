using System;
using UnityEngine;

public static class RigidBodyExtensions
{
    public static void AddMagnusForce(this Rigidbody2D rigidbody, float density, float signedDeltaRotation, float transverseArea, ForceMode2D mode)
        => rigidbody.AddForce(GetMagnusForce(rigidbody, density, signedDeltaRotation, transverseArea), mode);

    public static Vector2 GetMagnusForce(
        this Rigidbody2D rigidbody, 
        float density, 
        float signedDeltaRotation, 
        float transverseArea)
    {
        Vector2 forceDirection = Vector2.Perpendicular
            (Math.Sign(signedDeltaRotation) * rigidbody.velocity).normalized;

        float angularVelocity = Mathf.Abs(signedDeltaRotation) * Mathf.Deg2Rad;
        float linearVelocity = rigidbody.velocity.magnitude;
        float angularDrag = rigidbody.angularDrag;
        float forceValue = 2 
            * density 
            * angularVelocity * angularDrag 
            * linearVelocity 
            * transverseArea;

        return forceValue * forceDirection;
    }

    public static void MoveRotation(this Rigidbody2D rb, Vector3 euler)
    {
        rb.MoveRotation(Quaternion.Euler(euler));
    }
}
