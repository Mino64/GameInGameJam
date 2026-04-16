using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider;

    void Start()
    {
        // Load saved volume (default = 1)
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);

        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        // Listen for changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;

        // Save it
        PlayerPrefs.SetFloat("volume", value);
    }
}