using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPlayer : MonoBehaviour
{
    public Transform cameraTransform;
    public float speed = 5f;
    CharacterController characterController;

    void Awake() {
        characterController = GetComponent<CharacterController>();
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

        movement.Normalize();
        Move(movement);
    }

    public void Move(Vector3 direction) {
        characterController.Move(direction * speed * Time.deltaTime);
        transform.LookAt(transform.position + direction);
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Chair")) {
            SceneManager.LoadScene("First-Mission");
        }
    }
}