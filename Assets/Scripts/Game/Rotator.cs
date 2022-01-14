using UnityEngine;

[System.Serializable]
public class Rotator
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public bool storable;

    public float Rotation {
        get => rb.rotation;
        set { rb.rotation = value; }
    }

    public void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
        => rb.MoveRotation(rb.rotation + speed * Time.fixedDeltaTime);
}
