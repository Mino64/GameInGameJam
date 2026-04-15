using UnityEngine;
using System.Collections;

public class CameraSequencer : MonoBehaviour
{
    [SerializeField] private float zoomedInSize = 3f;   // orthographic size when zoomed into a puzzle
    [SerializeField] private float zoomedOutSize = 7f;  // orthographic size when showing all puzzles
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float moveSpeed = 3f;

    private Camera cam;
    private Coroutine sequence;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = zoomedOutSize; // start zoomed out
    }

    public void ZoomToPuzzle(Vector3 targetPosition)
    {
        if (sequence != null) StopCoroutine(sequence);
        sequence = StartCoroutine(ZoomOutThenIn(targetPosition));
    }

    IEnumerator ZoomOutThenIn(Vector3 targetPosition)
    {
        // zoom out
        yield return StartCoroutine(ZoomTo(zoomedOutSize));

        // move to target position (keep camera Z)
        yield return StartCoroutine(MoveTo(new Vector3(targetPosition.x, targetPosition.y, transform.position.z)));

        // zoom in
        yield return StartCoroutine(ZoomTo(zoomedInSize));
    }

    IEnumerator ZoomTo(float targetSize)
    {
        while (!Mathf.Approximately(cam.orthographicSize, targetSize))
        {
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
            yield return null;
        }
        cam.orthographicSize = targetSize;
    }

    IEnumerator MoveTo(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
    }
}
