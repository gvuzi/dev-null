using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class MainMenuHandler : MonoBehaviour
{


    [Header("Canvas")]
    public Canvas mainMenu;
    public Canvas optionsMenu;
    public Canvas volumeSettings;
    public Canvas videoSettings;

    [Header("Audio")]
    public AudioMixer mixer;
    public AudioSource sfxSource;
    public AudioClip clickSound;
    public Button startButton;
    public Button optionsButton;
    public Button quitButton;

    [Header("Resolution")]
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start() {
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        List<string> resolutionOptions = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = resolutions.Length - 1 ; i >= 0; i--) {
            string resolutionOption = resolutions[i].width + "x" + resolutions[i].height;
            resolutionOptions.Add(resolutionOption);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = resolutions.Length - 1 - i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void StartGame() {
        StartCoroutine(AudioRoutine("Hub-Dorm", startButton));
    }

    public void OptionsMenu() {
        playClickSound();

        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void QuitGame() {
        StartCoroutine(AudioRoutine(null, quitButton));
    }

    // options
    public void MainMenuBack() {
        playClickSound();

        optionsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void VolumeSettings() {
        playClickSound();

        optionsMenu.gameObject.SetActive(false);
        volumeSettings.gameObject.SetActive(true);
    }

    public void VolumeBack() {
        playClickSound();

        volumeSettings.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void setMasterVolume(float volume) {
        mixer.SetFloat("masterVolume", volume);
    }

    public void setMusicVolume(float volume) {
        mixer.SetFloat("musicVolume", volume);
    }

    public void setSfxVolume(float volume) {
        mixer.SetFloat("sfxVolume", volume);
    }

    public void VideoSettings() {
        playClickSound();

        optionsMenu.gameObject.SetActive(false);
        videoSettings.gameObject.SetActive(true);
    }
    public void VideoBack() {
        playClickSound();

        videoSettings.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void setFullScreen(int index) {
        if (index == 0) {
            Screen.fullScreen = true;
        }
        else {
            Screen.fullScreen = false;
        }
    }

    public void setResolution(int index) {
        int reversedIndex = resolutions.Length - 1 - index;
        Resolution resolution = resolutions[reversedIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setVSync(int index) {
        if (index == 0) {
            QualitySettings.vSyncCount = 0;
        }
        else {
            QualitySettings.vSyncCount = 1;
        }
    }

    public void playClickSound() {
        sfxSource.resource = clickSound;
        sfxSource.Play();
    }

    IEnumerator AudioRoutine(string scene, Button button) {
        button.interactable = false;

        playClickSound();

        while (sfxSource.isPlaying == true) {
            yield return null;
        }

        if (scene != null) {
            SceneManager.LoadScene(scene);
        }
        else {
            Application.Quit();
        }
        
        button.interactable = true; // set for demo purposes, not applicable in-game
    }



}
