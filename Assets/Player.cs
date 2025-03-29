using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    public Camera firstPersonCamera;
    public LayerMask terrain;
    public Transform groundCheckTransform;

    [Header("Movement")]
    CharacterController characterController;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpSpeed = 5f;
    Vector3 gravityVelocity = Vector3.zero;

    [Header("Bullets")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 45f;
    public float shootTime = 1f;
    public Transform startPoint;
   

    void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {

    }

    void Update()
    {
        ApplyGravity();
    }

    public void Move(Vector3 direction) {
        characterController.Move(direction * speed * Time.deltaTime);
        transform.LookAt(transform.position + direction);
    }

    public void Jump() {
        if(PlayerOnGround()) {
            gravityVelocity.y = jumpSpeed;
        }
        return;
    }

    public bool PlayerOnGround() {
        return Physics.OverlapSphere(groundCheckTransform.position,0.5f,terrain).Length > 0;
    }

    public void ApplyGravity() {
        if(characterController.isGrounded && gravityVelocity.y < 0){ 
            gravityVelocity = Vector3.zero;
            return;
        }
        gravityVelocity.y += gravity * Time.deltaTime;
        characterController.Move(gravityVelocity * Time.deltaTime);
    }

    public void Shoot(){
        Ray ray = firstPersonCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //middle of screen (crosshair)
        RaycastHit hit;

        Vector3 endPoint;
        if (Physics.Raycast(ray, out hit)) {
            endPoint = hit.point;
        }
        else {
            endPoint = ray.GetPoint(70);
        }

        GameObject bullet = Instantiate(bulletPrefab, startPoint.position, Quaternion.identity);
        StartCoroutine(ShootRoutine(bullet, endPoint));
    }

    IEnumerator ShootRoutine(GameObject bullet, Vector3 endPoint) {
        float distance = Vector3.Distance(startPoint.position, endPoint);
        float shootTime = distance / bulletSpeed;

        float t = 0;
        while(t < shootTime) {
            t += Time.deltaTime;
            bullet.transform.position = Vector3.Lerp(startPoint.position,endPoint,t/shootTime);
            yield return null;
        }
        bullet.transform.position = endPoint;
        Destroy(bullet, 1f);
        yield return null;
    }


    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Chair")) {
            SceneManager.LoadScene("First-Mission");
        }
    }
}
