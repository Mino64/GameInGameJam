using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private CylceButton[] puzzles;
    [SerializeField] private string nextScene;
    [SerializeField] private CameraSequencer cameraSequencer;

    private int currentPuzzleIndex = 0;

    void Start()
    {
        foreach (var puzzle in puzzles)
            puzzle.OnPuzzleSolved += OnPuzzleSolved;

        // activate the first puzzle with sound immediately
        puzzles[0].Activate(true);

        if (puzzles.Length > 1)
            cameraSequencer.ZoomToPuzzle(puzzles[currentPuzzleIndex].transform.position);
    }

    void OnPuzzleSolved()
    {
        currentPuzzleIndex++;

        if (currentPuzzleIndex >= puzzles.Length)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            if (puzzles.Length > 1)
                // activate next puzzle with sound only after camera finishes moving
                cameraSequencer.ZoomToPuzzle(puzzles[currentPuzzleIndex].transform.position, () => puzzles[currentPuzzleIndex].Activate(true));
            else
                puzzles[currentPuzzleIndex].Activate(true);
        }
    }

    void OnDestroy()
    {
        foreach (var puzzle in puzzles)
            puzzle.OnPuzzleSolved -= OnPuzzleSolved;
    }
}