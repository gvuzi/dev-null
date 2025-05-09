using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MissionEndHandler : MonoBehaviour
{
    public Canvas initialPrompt;
    public Timer timer;
    
    public TextMeshProUGUI[] outputText;
    public TextMeshProUGUI initialText;
    public TextMeshProUGUI time;
    public TextMeshProUGUI continueText;
    public AudioClip endMusic;
   
    public Player player;

    void Start() {
        player.isPaused = true;
        player.musicAudioSource.Pause();
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence() {
        initialPrompt.gameObject.SetActive(true);
        initialText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        initialText.gameObject.SetActive(false);

        for (int i = 0; i < outputText.Length; i++) {
            outputText[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            outputText[i].gameObject.SetActive(false);
        }

        time.text = "time taken: " + timer.getTime();
        time.gameObject.SetActive(true);
        continueText.gameObject.SetActive(true);
    }
}

