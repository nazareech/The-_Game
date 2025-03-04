using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Image soundOnIcon;
    [SerializeField] private Image soundOffIcon;
    [SerializeField] private GameObject blocSliderValue;

    private bool muted;

    void Start()
    {
        // Завантаження налаштувань або встановлення значень за замовчуванням
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        muted = PlayerPrefs.GetInt("muted", 0) == 1;

        ApplySettings();
    }

    public void OnButtonPress()
    {
        muted = !muted; // Інвертуємо mute
        ApplySettings();
        SaveSettings();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveSettings();
    }

    private void ApplySettings()
    {
        AudioListener.pause = muted;
        soundOnIcon.enabled = !muted;
        soundOffIcon.enabled = muted;

        blocSliderValue.SetActive(muted);
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
        PlayerPrefs.Save(); // Зберігаємо налаштування
    }
}
