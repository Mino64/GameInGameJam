using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedSceneChange : MonoBehaviour
{
    [SerializeField]
    private float timer;

    [SerializeField]
    private string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("SwapScene");
    }

    // Update is called once per frame
    private IEnumerator SwapScene()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(sceneName);
    }
}
