using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Player player;
    public Transform cameraTransform;
    public PromptHandler initialPrompt;
    public PauseMenuHandler pauseMenu;

    private bool promptGone = false;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0;

        if (Input.GetKey(KeyCode.W))
        {
            movement += cameraForward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement -= cameraForward;
        }

        if (Input.GetKey(KeyCode.A)){
            movement -= cameraRight;
        }

        if(Input.GetKey(KeyCode.D)){
            movement += cameraRight;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Mouse ScrollWheel") > 0f) {
            player.Jump();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            if (player.isPaused) {
                return;
            }
                player.Shoot();
        }

        if(Input.GetKey(KeyCode.Mouse0)) {
            if (player.isPaused) {
                return;
            }
                player.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0f;
        }

        if(Input.GetKeyDown(KeyCode.Return)) {
            if (promptGone) {
                return;
            } 

            initialPrompt.playEnterSound();
            initialPrompt.Hide();
            promptGone = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!promptGone) {
                return;
            } 

            if (player.isPaused) {
                player.musicAudioSource.UnPause();
                pauseMenu.Back();
            }
            else {
                player.musicAudioSource.Pause();
                pauseMenu.Pause();
            }
        }

        movement.Normalize();
        player.Move(movement);
    }
}
