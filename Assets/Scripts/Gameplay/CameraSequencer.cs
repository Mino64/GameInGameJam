using UnityEngine;
using System.Collections;

public class CameraSequencer : MonoBehaviour
{
    [SerializeField] private float zoomedInSize = 3f;
    [SerializeField] private float zoomedOutSize = 7f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float moveSpeed = 3f;

    private Camera cam;
    private Coroutine sequence;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = zoomedOutSize;
    }

    public void ZoomToPuzzle(Vector3 targetPosition, System.Action onComplete = null)
    {
        if (sequence != null) StopCoroutine(sequence);
        sequence = StartCoroutine(ZoomOutThenIn(targetPosition, onComplete));
    }

    IEnumerator ZoomOutThenIn(Vector3 targetPosition, System.Action onComplete = null)
    {
        yield return StartCoroutine(ZoomTo(zoomedOutSize));
        yield return StartCoroutine(MoveTo(new Vector3(targetPosition.x, targetPosition.y, transform.position.z)));
        yield return StartCoroutine(ZoomTo(zoomedInSize));
        onComplete?.Invoke();
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