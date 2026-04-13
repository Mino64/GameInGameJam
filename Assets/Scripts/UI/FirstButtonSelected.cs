using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FirstButtonSelected : MonoBehaviour
{
    [SerializeField]
    private Button firstSelected;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
    }
}
