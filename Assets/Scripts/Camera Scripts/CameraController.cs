using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    private const float lookSpeed = 2.0f;
    private const float lookXLimit = 60.0f;

    Vector2 rotation = Vector2.zero;

    // Camera bobbing variables
    public float walkBobbingSpeed = 10f;
    public float walkBobbingAmount = 0.05f;
    public float runBobbingSpeed = 18f;
    public float runBobbingAmount = 0.1f;

    private bool isRunning = false;
    private float bobbingSpeed;
    private float bobbingAmount;
    private float midpoint;
    private float timer = 0f;

    private void Start()
    {
        GetRotationY();

        bobbingSpeed = walkBobbingSpeed;
        bobbingAmount = walkBobbingAmount;
        midpoint = transform.localPosition.y;
    }

    private float GetRotationY()
    {
        rotation.y = playerTransform.eulerAngles.y;
        return rotation.y; 
    }

    private void Update()
    {
        CameraRotation();
        ApplyCameraBobbing();
    }

    private void CameraRotation()
    {
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        playerTransform.eulerAngles = new Vector2(0, rotation.y);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            bobbingSpeed = runBobbingSpeed;
            bobbingAmount = runBobbingAmount;
        }
        else
        {
            isRunning = false;
            bobbingSpeed = walkBobbingSpeed;
            bobbingAmount = walkBobbingAmount;
        }
    }

    private void ApplyCameraBobbing()
    {
        float waveSlice = 0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0f;
        }
        else
        {
            waveSlice = Mathf.Sin(timer);
            timer += bobbingSpeed * Time.deltaTime;

            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }
        }

        if (waveSlice != 0)
        {
            float translateChange = waveSlice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange *= totalAxes;
            Vector3 localPosition = transform.localPosition;
            localPosition.y = midpoint + translateChange;
            transform.localPosition = localPosition;
        }
        else
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.y = midpoint;
            transform.localPosition = localPosition;
        }
    }
}