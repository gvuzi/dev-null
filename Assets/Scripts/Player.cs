using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    public Camera firstPersonCamera;
    public LayerMask terrain;
    public Transform groundCheckTransform;
    public bool isPaused;

    [Header("Movement")]
    CharacterController characterController;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpSpeed = 5f;
    Vector3 gravityVelocity = Vector3.zero;

    [Header("Bullets")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 25f;
    public float shootTime = 1f;
    public float shootCooldown = 0.5f;
    private bool canShoot = true;
    public Transform startPoint;

    [Header("Health")]
    private float maxHealth = 100f;
    private float currentHealth;
    private float damage = 25f;
    public Healthbar healthbar;

    [Header("Mechanics")]
    public float dataFragmentsCollected = 0;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip damageSound;
    public AudioClip pickupSound;
    public AudioSource musicAudioSource;
   

    void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealth(maxHealth, currentHealth);
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
        if (!canShoot) return;

        Ray ray = firstPersonCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //middle of screen (crosshair)
        RaycastHit hit;

        Vector3 endPoint;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.CompareTag("Enemy")) {
                endPoint = hit.point + ray.direction * 1f; // extend the ray for hit detection 
            }
            else {
                endPoint = hit.point;
            }
        } 
        else {
            endPoint = ray.GetPoint(70);
        }

        GameObject bullet = Instantiate(bulletPrefab, startPoint.position, Quaternion.identity);
        Vector3 direction = (endPoint - startPoint.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        bullet.transform.rotation = lookRotation * Quaternion.Euler(90f, 270f, 0f);

        StartCoroutine(ShootRoutine(bullet, endPoint));
        StartCoroutine(ShootingCooldown());
    }

    IEnumerator ShootingCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
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
    
        Destroy(bullet);
    }

    public void HitSound() {
        // audioSource.resource = hitSound;
        audioSource.PlayOneShot(hitSound);
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("EnemyBullet")) {
            // audioSource.resource = damageSound;
            audioSource.PlayOneShot(damageSound);
            currentHealth -= damage;
            healthbar.UpdateHealth(maxHealth, currentHealth); 
        } 
        
        if (currentHealth <= 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } 
        

        if (other.CompareTag("Spike") || other.CompareTag("Enemy")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.CompareTag("DataFragment")) {
            audioSource.PlayOneShot(pickupSound);
            Destroy(other.gameObject);
            dataFragmentsCollected++;
            currentHealth = 100f;
            healthbar.UpdateHealth(maxHealth, currentHealth); 
        }

        if (other.CompareTag("Door")) {
            if (dataFragmentsCollected == 3) {
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("NPC")) {
            SceneManager.LoadScene("Hub-Dorm");
        }
    }


    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Door"))
        {
            if (dataFragmentsCollected == 3) {
                Destroy(hit.gameObject);
            }
        }
    }

}
