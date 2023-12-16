using UnityEngine;

public class CameraColision : MonoBehaviour
{
    public Transform referenceTransform;
    public float collisionOffset = 0.3f; 
    public float cameraSpeed = 15f;

    Vector3 defaultPos;
    Vector3 directionNormalized;
    Transform parentTransform;
    float defaultDistance;
    
    void Start()
    {
        CameraInit();
    }

    private void CameraInit()
    {
        defaultPos = transform.localPosition;
        directionNormalized = defaultPos.normalized;
        parentTransform = transform.parent;
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);
    }
    
    private void LateUpdate()
    {
        UpdateTransPos();
    }

    private void UpdateTransPos()
    {
        Vector3 currentPos = defaultPos;
        RaycastHit hit;
        Vector3 dirTmp = parentTransform.TransformPoint(defaultPos) - referenceTransform.position;

        if (Physics.SphereCast(referenceTransform.position, collisionOffset, dirTmp, out hit, defaultDistance))
        {
            currentPos = (directionNormalized * (hit.distance - collisionOffset));
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * cameraSpeed);
    }
}