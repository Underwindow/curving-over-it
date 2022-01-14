using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class VanishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject vanishingObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerPrefs.HasKey("DELIVERY_IS_GONE"))
            return;

        if (other.tag == "Player")
        {
            vanishingObject?.SetActive(false);
            PlayerPrefs.SetInt("DELIVERY_IS_GONE", 1);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("DELIVERY_IS_GONE"))
        {
            vanishingObject?.SetActive(false);
        }
    }
}
