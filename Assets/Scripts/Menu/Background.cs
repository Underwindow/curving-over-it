using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Background : MonoBehaviour
{
    [SerializeField] Material idling, loading;

    private Image image;

    public void OnLoadingNewScene()
    {
        image.material = loading;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }
}
