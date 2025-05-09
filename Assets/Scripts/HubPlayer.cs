using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPlayer : MonoBehaviour
{
    public Transform cameraTransform;
    public float speed = 5f;
    CharacterController characterController;
    public PauseMenuHandler pauseMenu;
    public PromptHandler initialPrompt;
    public bool isPaused = false;
    private bool promptGone = false;


    [Header("Animation")]
    public PlayerAnimationChanger PlayerAnimationChanger;
    public string idleAnimationState = "PlayerIdle";
    public string walkAnimationState = "PlayerWalk";

    void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    void Start() {
        ChangeAnimationState(idleAnimationState);
        Time.timeScale = 1f;
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
            
            if (isPaused) {
                pauseMenu.Back();
            }
            else {
                pauseMenu.Pause();
            }
        }

        

        movement.Normalize();
        Move(movement);
    }

    public void Move(Vector3 direction) {
        characterController.Move(direction * speed * Time.deltaTime);
        if (direction == Vector3.zero || isPaused) {
            ChangeAnimationState(idleAnimationState);
            return;
        }
        
        ChangeAnimationState(walkAnimationState);
        transform.LookAt(transform.position + direction);
        
    }

    void ChangeAnimationState(string newAnimationState) {
        PlayerAnimationChanger.ChangeAnimationState(newAnimationState);
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Chair")) {
            SceneManager.LoadScene("First-Mission");
        }
    }
}