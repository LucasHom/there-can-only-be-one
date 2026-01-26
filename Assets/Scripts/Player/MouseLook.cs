using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity = 1000f;
    [SerializeField] private Transform playerBody;

    [Header("Head Bob Settings")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float bobAmount = 0.05f;
    [SerializeField] private float bobSpeed = 10f;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchCameraOffset = -0.5f;

    private float xRotation;
    //starting Y position of the camera
    private float defaultY;
    // current base Y position of the camera
    private float baseY;
    private float bobTimer;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        defaultY = cameraTransform.localPosition.y;
        baseY = defaultY;

        playerMovement.OnPlayerMove += PlayerMovement_BobWhileMoving;
        playerMovement.OnCrouchToggled += PlayerMovement_OnCrouchToggled;
    }

    void Update()
    {
        HandleMouseLook();
    }

    //Mouse look
    private void HandleMouseLook()
    {
        float delta = Time.deltaTime;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * delta;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * delta;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    //Bobbin
    private void PlayerMovement_BobWhileMoving(object sender, PlayerMovement.OnPlayerMoveEventArgs e)
    {
        if (!e.isGrounded)
        {
            ResetBob();
            return;
        }

        float horizontalSpeed = new Vector2(e.velocity.x, e.velocity.z).magnitude;

        if (horizontalSpeed > 0.01f)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            float yOffset = Mathf.Sin(bobTimer) * bobAmount;
            SetCameraY(baseY + yOffset);
        }
        else
        {
            ResetBob();
        }
    }

    private void ResetBob()
    {
        bobTimer = 0f;
        SetCameraY(baseY);
    }

    //Crouch
    private void PlayerMovement_OnCrouchToggled(object sender, PlayerMovement.OnCrouchEventArgs e)
    {
        bobTimer = 0f;

        baseY = e.isCrouching
            ? defaultY + crouchCameraOffset
            : defaultY;

        SetCameraY(baseY);
    }

    //Camera support func
    private void SetCameraY(float y)
    {
        Vector3 pos = cameraTransform.localPosition;
        pos.y = y;
        cameraTransform.localPosition = pos;
    }
}
