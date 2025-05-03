using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class MainMenuHandler : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip clickSound;
    public Button startButton;
    public Button optionsButton;
    public Button quitButton;


    public void StartGame() {
        StartCoroutine(AudioRoutine("Hub-Dorm", startButton));
    }

    public void OptionsMenu() {
        StartCoroutine(AudioRoutine("Options-Menu", optionsButton));
    }

    public void QuitGame() {
        StartCoroutine(AudioRoutine(null, quitButton));
    }

    IEnumerator AudioRoutine(string scene, Button button) {
        button.interactable = false;

        audioSource.resource = clickSound;
        audioSource.Play();

        while (audioSource.isPlaying == true) {
            yield return null;
        }

        if (scene != null) {
            SceneManager.LoadScene(scene);
        }

        Application.Quit();
        button.interactable = true; // set for demo purposes, not applicable in-game
    }


}
