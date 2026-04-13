using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
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
        if (currentIndex == randomNum)
        {
            SceneManager.LoadScene(sceneName);
            Log($"Changing Scene");
        }

        UpdateSymbol();
    }

    void UpdateSymbol()
    {
        display.sprite = symbols[currentIndex];
    }

}
