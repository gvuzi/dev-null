using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public Player player; 
    public float followSpeed = 3f; 
    public float followRange = 15f;

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > followRange) {
            return;
        }

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        
        transform.LookAt(player.transform);
        transform.position += direction * followSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Bullet")) {
            player.HitSound();
            Destroy(this.gameObject);
        }
    }
}
