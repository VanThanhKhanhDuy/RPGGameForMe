using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    private const float lookSpeed = 2.0f;
    private const float lookXLimit = 60.0f;

    Vector2 rotation = Vector2.zero;

    private void Start()
    {
        GetRotationY();
    }

    private float GetRotationY()
    {
        rotation.y = playerTransform.eulerAngles.y;
        return rotation.y; 
    }

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        playerTransform.eulerAngles = new Vector2(0, rotation.y);
    }
}