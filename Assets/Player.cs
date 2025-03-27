using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    CharacterController characterController;
    public float speed = 5f;

    void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 direction) {
        characterController.Move(direction * speed * Time.deltaTime);
        transform.LookAt(transform.position + direction);
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Chair")) {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
