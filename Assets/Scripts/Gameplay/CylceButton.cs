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

    private int currentIndex = 0;
    private int randomNum;
    private Coroutine timer;

    void Start()
    {
        rotationPerStep = 360f / symbols;
        randomNum = Random.Range(0, symbols);
        Log($"Target index: {randomNum}");


    }

    public void OnButtonPressed()
    {
        currentIndex = (currentIndex + 1) % symbols;
        Log($"Current Index: {currentIndex}");

        display.transform.rotation = UnityEngine.Quaternion.Euler(0f, 0f, -rotationPerStep*currentIndex);

        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }

        if (currentIndex == randomNum)
        {
            Log($"Starting timer");
            timer = StartCoroutine(WaitAndLoad());
        }
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(waitTime);
        Log($"Changing Scene");
        SceneManager.LoadScene(sceneName);
    }

}
