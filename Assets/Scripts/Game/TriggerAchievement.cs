using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerAchievement : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnTriggerEnter;

    public string collisionAchievementName;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter?.Invoke();
    }
}
