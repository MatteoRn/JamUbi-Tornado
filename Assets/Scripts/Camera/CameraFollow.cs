using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Settings")] 
    public float followSpeed = 3f;
    public float offsetY = 1;

    void LateUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + offsetY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
