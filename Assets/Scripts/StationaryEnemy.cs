using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StationaryEnemy : MonoBehaviour
{
    public Player player;
    public Transform enemy;

    [Header("Bullets")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 45f;
    public float shootTime = 1f;
    public float shootCooldown = 2f;
    public Transform startPoint;
    private List<GameObject> bullets = new List<GameObject>();
    

    // velocity variables
    Vector3 playerVelocity;
    Vector3 playerLastPosition;
    Vector3 playerCurrentPosition;
   
    void Start()
    {
        playerLastPosition = player.transform.position;
        InvokeRepeating("Shoot", 0f, shootCooldown);
    }

    void Update()
    {
        //calculate player's velocity
        playerCurrentPosition = player.transform.position;
        playerVelocity = (playerCurrentPosition - playerLastPosition) / Time.deltaTime;
        playerLastPosition = playerCurrentPosition;
        
        transform.LookAt(player.transform.position);
    }

    void Shoot() {
        if(Vector3.Distance(enemy.transform.position, player.transform.position) > 20) {
            return;
        }
       
        Ray ray = new Ray(startPoint.position, (player.transform.position - startPoint.position).normalized);
        RaycastHit hit;

        Vector3 endPoint;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.CompareTag("Player")) {
                endPoint = CalculateEndPoint(); 
            }
            else {
                endPoint = hit.point;
            }
        } 
        else {
            endPoint = ray.GetPoint(70);
        }

        GameObject bullet = Instantiate(bulletPrefab, startPoint.position, Quaternion.identity);
        bullets.Add(bullet);
        StartCoroutine(ShootRoutine(bullet, endPoint));
    }

    Vector3 CalculateEndPoint() {
        // calculate distance from spawn point to player
        float distance = Vector3.Distance(startPoint.position, player.transform.position);

        // calculate bullet fly time, time it takes to reach player's pos
        float bulletFlyTime = distance / bulletSpeed;

        // calculates end point (where player will be)
        Vector3 endPoint = player.transform.position + (playerVelocity * bulletFlyTime);

        // extend bullet path past the player
        float extendedDistance = 70f;
        Vector3 direction = (endPoint - startPoint.position).normalized;
        Vector3 extendedEndPoint = endPoint + (direction * extendedDistance);
        return extendedEndPoint;
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

        Destroy(bullet);
        yield return null;
    }

    void DestroyBullets() {
        for(int i = 0; i < bullets.Count; i++) {
            Destroy(bullets[i]);
        }
        bullets.Clear();
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Bullet")) {
            Destroy(this.gameObject);
            DestroyBullets();
        }
    }

}
