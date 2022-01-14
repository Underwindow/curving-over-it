using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    private string english, russian;

    [SerializeField]
    private bool isStatic = true;

    [HideInInspector]
    public string Text => GameLocalization.Instance.Language == UserLanguage.eng ? english : russian;

    void Start()
    {
        if (isStatic) {
            var textComponent = GetComponent<TextMeshProUGUI>();
                textComponent.text = Text;
        }
    }
}