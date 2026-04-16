using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.Debug;
using DG.Tweening;

public class CylceButton : MonoBehaviour
{
    [SerializeField] private Image display;
    [SerializeField] private int symbols = 6;
    [SerializeField] private float waitTime = 6f;
    [SerializeField] private float rotationPerStep;
    [SerializeField] private int winNum;

    [Header("Arrows")]
    [SerializeField] private GameObject arrowDefault;
    [SerializeField] private GameObject arrowCorrect;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] symbolSounds;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip puzzleCompleteSound;

    [Header("Arrow Wiggle")]
    [SerializeField] private float wiggleStrength = 15f;
    [SerializeField] private int wiggleVibrato = 10;
    [SerializeField] private float wiggleDuration = 0.5f;

    private Coroutine timer;
    private int currentIndex = 0;

    public event System.Action OnPuzzleSolved;

    void Start()
    {
        rotationPerStep = 360f / symbols;
        arrowDefault.SetActive(true);
        arrowCorrect.SetActive(false);
    }

    public void Activate(bool playSound)
    {
        ShakeArrows();
        if (playSound && correctSound != null)
            audioSource.PlayOneShot(correctSound);
    }

    public void OnButtonPressed()
    {
        currentIndex = (currentIndex + 1) % symbols;
        Log($"Current Index: {currentIndex}");

        display.transform.rotation = UnityEngine.Quaternion.Euler(0f, 0f, -rotationPerStep * currentIndex);

        if (symbolSounds != null && currentIndex < symbolSounds.Length)
            audioSource.PlayOneShot(symbolSounds[currentIndex]);

        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }

        if (currentIndex == winNum)
        {
            arrowDefault.SetActive(false);
            arrowCorrect.SetActive(true);
            Log($"Starting timer");
            timer = StartCoroutine(WaitAndSolve());
        }
        else
        {
            arrowDefault.SetActive(true);
            arrowCorrect.SetActive(false);
        }
    }

    public void OnArrowPressed()
    {
        if (correctSound != null)
            audioSource.PlayOneShot(correctSound);

        ShakeArrows();
    }

    void ShakeArrows()
    {
        arrowCorrect.transform.DOKill();
        arrowCorrect.transform.DOShakePosition(wiggleDuration, new Vector3(wiggleStrength, 0f, 0f), wiggleVibrato);

        arrowDefault.transform.DOKill();
        arrowDefault.transform.DOShakePosition(wiggleDuration, new Vector3(wiggleStrength, 0f, 0f), wiggleVibrato);
    }

    IEnumerator WaitAndSolve()
    {
        yield return new WaitForSeconds(waitTime);

        if (puzzleCompleteSound != null)
        {
            audioSource.PlayOneShot(puzzleCompleteSound);
            yield return new WaitForSeconds(puzzleCompleteSound.length);
        }

        Log($"Puzzle solved!");
        OnPuzzleSolved?.Invoke();
    }
}