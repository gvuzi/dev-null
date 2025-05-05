using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPlayer : MonoBehaviour
{
    public Transform cameraTransform;
    public float speed = 5f;
    CharacterController characterController;
    public PauseMenuHandler PauseMenu;
    public bool isPaused;

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

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                PauseMenu.Back();
                isPaused = false;
            }
            else {
                PauseMenu.Pause();
                isPaused = true;
            }
        }

        movement.Normalize();
        Move(movement);
    }

    public void Move(Vector3 direction) {
        characterController.Move(direction * speed * Time.deltaTime);
        if (direction == Vector3.zero) {
            ChangeAnimationState(idleAnimationState);
            return;
        }
        
        if (!isPaused) {
            ChangeAnimationState(walkAnimationState);
            transform.LookAt(transform.position + direction);
        }
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