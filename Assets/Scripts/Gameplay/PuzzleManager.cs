using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private CylceButton[] puzzles; // drag puzzles in order
    [SerializeField] private string nextScene;
    [SerializeField] private CameraSequencer cameraSequencer;

    private int currentPuzzleIndex = 0;

    void Start()
    {
        // subscribe to each puzzle's solved event
        foreach (var puzzle in puzzles)
            puzzle.OnPuzzleSolved += OnPuzzleSolved;

        // zoom into the first puzzle automatically
        cameraSequencer.ZoomToPuzzle(puzzles[currentPuzzleIndex].transform.position);
    }

    void OnPuzzleSolved()
    {
        currentPuzzleIndex++;

        if (currentPuzzleIndex >= puzzles.Length)
        {
            // all puzzles done, load next scene
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            // move camera to next puzzle
            cameraSequencer.ZoomToPuzzle(puzzles[currentPuzzleIndex].transform.position);
        }
    }

    void OnDestroy()
    {
        foreach (var puzzle in puzzles)
            puzzle.OnPuzzleSolved -= OnPuzzleSolved;
    }
}
