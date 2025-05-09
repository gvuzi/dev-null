using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 75f;
    void Update()
    {
        Vector3 rotation = Vector3.zero;
        rotation += Vector3.up;
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }
}
