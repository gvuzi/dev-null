using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PromptHandler : MonoBehaviour
{
    public Canvas initialPrompt;
    public TextMeshProUGUI command;
    public TextMeshProUGUI[] outputText;
    public AudioSource sfxSource;
    public AudioClip enterSound;
    public HubPlayer player;
    public GameObject mainScene;

    


    void Start() {
        player.isPaused = true;
        StartCoroutine(InitializePrompt());
        player.isPaused = false;
    }

    IEnumerator InitializePrompt() {
        initialPrompt.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        command.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        playEnterSound();
        showText(outputText);
    }

    public void playEnterSound() {
        sfxSource.resource = enterSound;
        sfxSource.Play();
    }

    public void Hide() {
        initialPrompt.gameObject.SetActive(false);
        mainScene.SetActive(true);
    }

    private void showText(TextMeshProUGUI[] textGroup) {
        for (int i = 0; i < textGroup.Length; i++) {
            textGroup[i].gameObject.SetActive(true);
        }
    }
}
