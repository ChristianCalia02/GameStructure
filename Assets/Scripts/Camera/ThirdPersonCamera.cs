using Cam;
using UnityEngine;
using UnityEngine.Assertions;

public class ThirdPersonCamera : CameraBase
{



    [Tooltip("X: offset along camera's right vector, Y: vertical offset transformed by the target")]
    public Vector3 cameraOffsets { get; private set; } = new Vector3(0.6f, 1.65f, 0f);
    public bool flipSide { get; private set; } = false;



    [Header("Camera parameters")]
    [SerializeField]
    private float targetDistance = 5.0f;
    [SerializeField]
    [Range(0.0f, 90.0f)]
    private float maxPitch = 75.0f;
    [SerializeField]
    [Range(-90.0f, 0.0f)]
    private float minPitch = -85.0f;
    private float yawAngle = 0.0f;
    private float pitchAngle = 0.0f;

    [Header("Smoothing parameters")]
    [SerializeField]
    [Min(0.0f)]
    private float reachTime = 0.25f;
    private Vector3 smoothVelocity = Vector3.zero;
    private Vector3 currentFocusPoint = Vector3.zero;
    private Quaternion currentRotation;

    [Header("Collision detection")]
    [SerializeField]
    private bool checkCollisions = true;
    [SerializeField]
    private LayerMask collisionCheckLayers = Physics.AllLayers;

    private Vector3 CalculateFocusPoint() =>
    target.TransformPoint(Vector3.up * cameraOffsets.y) +
    (flipSide ? currentRotation * (-Vector3.right) : currentRotation * Vector3.right) * cameraOffsets.x;

    void LateUpdate()
    {
        AdaptRotation();
        AdaptPosition();
        CheckCollisions();
    }


    public override void ToggleLook() => flipSide = !flipSide;

    public override void AdaptToTarget()
    {
        Assert.IsNotNull(target);

        pitchAngle = 0.0f;

    }

    private void AdaptPosition()
    {
        if (target == null)
            return;

        Vector3 focusPoint = CalculateFocusPoint();

        //	Smooth focus point, if requested. Hard set otherwise.
        if (
            reachTime > Mathf.Epsilon &&
            (focusPoint - currentFocusPoint).sqrMagnitude > 0.0001f
        )
            currentFocusPoint = Vector3.SmoothDamp(
                currentFocusPoint,
                focusPoint,
                ref smoothVelocity,
                reachTime
            );
        else
            currentFocusPoint = focusPoint;

        //	Calculate and apply camera position
        Vector3 targetPosition = currentFocusPoint - (currentRotation * Vector3.forward) * targetDistance;

        transform.position = targetPosition;
    }
    private void AdaptRotation()
    {
        Quaternion yaw = Quaternion.AngleAxis(yawAngle, Vector3.up);
        Quaternion pitch = Quaternion.AngleAxis(pitchAngle, Vector3.right);

        currentRotation = yaw * pitch;
        transform.rotation = currentRotation;
    }
    public override void RotateYaw(float degrees)
    {
        yawAngle += degrees;
        yawAngle %= 360.0f;
    }

    public override void RotatePitch(float degrees)
    {
        pitchAngle += degrees;
        pitchAngle = Mathf.Clamp(pitchAngle, minPitch, maxPitch);
    }

    private void CheckCollisions()
    {
        if (!checkCollisions)
            return;


        Vector3 castOrigin = target.TransformPoint(Vector3.up * cameraOffsets.y);

        float near = cam.nearClipPlane;
        float angle = cam.fieldOfView / 2;
        float vertical = Mathf.Tan(angle * Mathf.Deg2Rad) * near;
        float horizontal = cam.aspect * vertical;
        float radius = new Vector3(horizontal, vertical, near).magnitude;

        //	Sphere cast from the cast origin towards the current camera position
        Vector3 distanceVector = transform.position - castOrigin;
        float distance = distanceVector.magnitude;
        Ray ray = new Ray(castOrigin, distanceVector);
        RaycastHit hit;
        bool anythingInBetween = Physics.SphereCast(
            ray,
            radius,
            out hit,
            distance,
            collisionCheckLayers
        );

        //	If nothing was hit, the camera is sage
        if (!anythingInBetween)
            return;

        //	If hit anything, move the camera to the last safe point on the ray
        transform.position = ray.GetPoint(hit.distance);
    }
}
