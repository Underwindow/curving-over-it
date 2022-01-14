using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(Vector3 deltaMovement);

    public ParallaxCameraDelegate onCameraTranslate;
    public Vector3 Position { get; private set; }

    private void Awake()
    {
        Position = transform.position;
    }

    private void Update()
    {
        if (transform.position.x != Position.x || transform.position.y != Position.y)
        {
            if (onCameraTranslate != null)
            {
                var delta = Position - transform.position;
                onCameraTranslate(delta);
            }
            Position = transform.position;
        }
    }
}
