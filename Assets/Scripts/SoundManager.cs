using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        else if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            
        }
        else if (PlayerPrefs.HasKey("muted"))
        {
            LoadButtonMusic();
        }
        else if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusic();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (muted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }

   
    public void ChangeVolume()
    {
        //---------
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void LoadButtonMusic()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void LoadMusic()
    {        
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("muted", muted ? 1 : 0);
        //-----------
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
