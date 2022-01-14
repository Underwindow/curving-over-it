using UnityEngine;

public class AngularDragController : MonoBehaviour
{
    private SafeFloat startDrag, endDrag;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startDrag = rb.angularDrag;
        endDrag = 6;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StopFading();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.rigidbody.CompareTag("Slide"))
        {
            rb.angularDrag = Mathf.Lerp(rb.angularDrag, endDrag.GetValue(), Time.fixedDeltaTime);
        }
        else 
        {
            StopFading();
        }
    }

    private void StopFading()
    {
        rb.angularDrag = startDrag.GetValue();
    }

    public void OnAimShot()
    {
        StopFading();
    }
}
