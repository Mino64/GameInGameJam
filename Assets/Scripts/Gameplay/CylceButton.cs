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
    private Sprite[] symbols;

    [SerializeField]
    private float waitTime = 6f;
    private int currentIndex = 0;
    private int randomNum;
    private Coroutine timer;

    void Start()
    {
        randomNum = Random.Range(0, symbols.Length);
        Log($"Target index: {randomNum}");
        UpdateSymbol();


    }

    public void OnButtonPressed()
    {
        currentIndex = (currentIndex + 1) % symbols.Length;
        Log($"Current Index: {currentIndex}");

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
        UpdateSymbol();
    }

    void UpdateSymbol()
    {
        display.sprite = symbols[currentIndex];
    }
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(waitTime);
        Log($"Changing Scene");
        SceneManager.LoadScene(sceneName);
    }

}
