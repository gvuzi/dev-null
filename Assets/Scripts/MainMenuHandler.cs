using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Hub-Dorm");
    }

    public void OptionsMenu() {
        SceneManager.LoadScene("Options-Menu");
    }

    public void QuitGame() {
        Application.Quit();
    }


}
