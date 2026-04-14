using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using static UnityEngine.Debug;




public class CylceButton : MonoBehaviour
{
    /// <summary>
    /// the sceneName is a string field that uses the Unity scene name
    /// </summary>
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private Image display;
    [SerializeField]
    private int symbols = 6;
    [SerializeField]
    private float waitTime = 6f;
    [SerializeField]
    private float rotationPerStep;
    [SerializeField]
    private int winNum;
    [SerializeField]
    private GameObject arrow;

    private Coroutine timer;
    private int currentIndex = 0;

    void Start()
    {
        rotationPerStep = 360f / symbols;


    }

    public void OnButtonPressed()
    {
        currentIndex = (currentIndex + 1) % symbols;
        Log($"Current Index: {currentIndex}");

        display.transform.rotation = UnityEngine.Quaternion.Euler(0f, 0f, -rotationPerStep * currentIndex);

        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }

        if (currentIndex == winNum)
        {
            arrow.SetActive(false);
            Log($"Starting timer");
            timer = StartCoroutine(WaitAndLoad());
        }
        else
        {
            arrow.SetActive(true);
        }
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(waitTime);
        Log($"Changing Scene");
        SceneManager.LoadScene(sceneName);
    }

}
