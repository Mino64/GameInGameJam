using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// the sceneName is a string field that uses the Unity scene name
    /// </summary>
    [SerializeField]
    private string _sceneName;

    public void PlayGame()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
