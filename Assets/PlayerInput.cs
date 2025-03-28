using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Player player;
    public Transform cameraTransform;

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

        if(Input.GetKeyDown(KeyCode.Space)) {
            player.Jump();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            player.Shoot();
        }

        movement.Normalize();
        player.Move(movement);
    }
}
