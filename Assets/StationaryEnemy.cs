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
    public Transform startPoint;
    private bool canShoot = true;
    void Start()
    {
        
    }

    void Update()
    {
        Shoot();
    }

    void Shoot() {
        if(Vector3.Distance(enemy.transform.position, player.transform.position) > 10) {
            return;
        }
        transform.LookAt(player.position);

        if(canShoot) {
        Vector3 endPoint = player.transform.position;
        GameObject bullet = Instantiate(bulletPrefab, startPoint.position, Quaternion.identity);
        StartCoroutine(ShootRoutine(bullet, endPoint));
        }
    }

    IEnumerator ShootRoutine(GameObject bullet, Vector3 endPoint) {
        float distance = Vector3.Distance(startPoint.position, endPoint);
        float shootTime = distance / bulletSpeed;

        float t = 0;

        canShoot = false;
        yield return new WaitForSeconds(1f);
        canShoot = true;

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
        if(other.CompareTag("Bullet")) {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
