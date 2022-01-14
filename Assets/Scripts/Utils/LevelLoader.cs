using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Delay in seconds before next scene starts loading")]
    private float loadDelay = 0;

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadLevelAsync(sceneIndex, loadDelay));
    }

    public void LoadLevel(int sceneIndex, float delay)
    {
        StartCoroutine(LoadLevelAsync(sceneIndex, delay));
    }

    private IEnumerator LoadLevelAsync (int sceneIndex, float delay = 0)
    {
        yield return new WaitForSeconds(delay);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }
}
