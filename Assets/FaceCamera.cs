using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public float rotateSpeed = 5f;

    void Update()
    {
        if (Camera.main == null) return;

        Vector3 targetPos = Camera.main.transform.position;
        targetPos.y = transform.position.y; // только по Y

        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
