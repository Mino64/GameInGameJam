using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using static UnityEngine.Debug;

public class CylceButton : MonoBehaviour
{
    [SerializeField] private Image display;
    [SerializeField] private int symbols = 6;
    [SerializeField] private float waitTime = 6f;
    [SerializeField] private float rotationPerStep;
    [SerializeField] private int winNum;
    [SerializeField] private GameObject arrow;

    private Coroutine timer;
    private int currentIndex = 0;

    public event System.Action OnPuzzleSolved; // PuzzleManager listens to this

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
            timer = StartCoroutine(WaitAndSolve());
        }
        else
        {
            arrow.SetActive(true);
        }
    }

    IEnumerator WaitAndSolve()
    {
        yield return new WaitForSeconds(waitTime);
        Log($"Puzzle solved!");
        OnPuzzleSolved?.Invoke();
    }
}