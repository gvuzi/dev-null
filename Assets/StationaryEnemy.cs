using UnityEngine;
using System.Collections;


public class StationaryEnemy : MonoBehaviour
{
    public Transform player;
    public Transform enemy;

    [Header("Bullets")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 45f;
    public float shootTime = 1f;
    public float shootCooldown = 2f;
    public Transform startPoint;

    // velocity variables
    Vector3 playerVelocity;
    Vector3 playerLastPosition;
    Vector3 playerCurrentPosition;
   
    void Start()
    {
        playerLastPosition = player.position;
        InvokeRepeating("Shoot", 0f, shootCooldown);
    }

    void Update()
    {
        //calculate player's velocity
        playerCurrentPosition = player.position;
        playerVelocity = (playerCurrentPosition - playerLastPosition) / Time.deltaTime;
        playerLastPosition = playerCurrentPosition;
        
    }

    void Shoot() {
        if(Vector3.Distance(enemy.transform.position, player.transform.position) > 20) {
            return;
        }

        transform.LookAt(player.position);

       
        Vector3 endPoint = CalculateEndPoint();
        GameObject bullet = Instantiate(bulletPrefab, startPoint.position, Quaternion.identity);
        StartCoroutine(ShootRoutine(bullet, endPoint));
        
    }

    Vector3 CalculateEndPoint() {
        // calculate distance from spawn point to player
        float distance = Vector3.Distance(startPoint.position, player.position);

        // calculate bullet fly time, time it takes to reach player's pos
        float bulletFlyTime = distance / bulletSpeed;

        // calculates end point (where player will be)
        Vector3 endPoint = player.position + (playerVelocity * bulletFlyTime);

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
        Destroy(bullet, 1f);
 
        yield return null;
    }


    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Bullet")) {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
