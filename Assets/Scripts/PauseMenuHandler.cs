using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;


public class PauseMenuHandler : MonoBehaviour
{
    public Canvas pauseMenu;
    public Button menuButton;
    public GameObject postProcessingVolume;
    public AudioSource sfxSource;
    public AudioClip clickSound;
    public HubPlayer hubPlayer;
    public Player player;
    private string currentScene;

    void Start() {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void Pause() {
        playClickSound();
        postProcessingVolume.SetActive(true);
        pauseMenu.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;

        if (currentScene == "Hub-Dorm" || currentScene == "Hub-Startup") {
            if (hubPlayer != null)
                hubPlayer.isPaused = true;
        }
        else {
            if (player != null)
                player.isPaused = true;
        } 

    }

    public void Back() {
        playClickSound();
        postProcessingVolume.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;

        if (currentScene == "Hub-Dorm" || currentScene == "Hub-Startup") {
            if (hubPlayer != null)
                hubPlayer.isPaused = false;
        }
        else {
            if (player != null)
                player.musicAudioSource.UnPause();
                player.isPaused = false;
        } 
    }

    public void MainMenu() {
        StartCoroutine(AudioRoutine("Main-Menu", menuButton));
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

        button.interactable = true; // set for demo purposes, not applicable in-game
    }
}