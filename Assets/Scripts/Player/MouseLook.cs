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

    private float xRotation = 0f;
    private float defaultY;
    private float bobTimer = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        defaultY = cameraTransform.localPosition.y;

        playerMovement.OnPlayerMove += PlayerMovement_BobWhileMoving;
    }

    void Update()
    {
        float delta = Time.deltaTime;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * delta;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * delta;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void PlayerMovement_BobWhileMoving(object sender, PlayerMovement.OnPlayerMoveEventArgs e)
    {
        if (!e.isGrounded)
        {
            bobTimer = 0f;
            cameraTransform.localPosition = new Vector3(
                cameraTransform.localPosition.x,
                defaultY,
                cameraTransform.localPosition.z
            );
            return;
        }

        //just ignoring the y value caus it doesnt effect how we bobbbin
        float speed = new Vector2(e.velocity.x, e.velocity.z).magnitude;

        if (speed > 0.01f)
        {
            //progressing bobtimer value along the sine wave
            bobTimer += Time.deltaTime * bobSpeed;
            float yOffset = Mathf.Sin(bobTimer) * bobAmount;

            cameraTransform.localPosition = new Vector3(
                cameraTransform.localPosition.x,
                defaultY + yOffset,
                cameraTransform.localPosition.z
            );
        }
        else
        {
            bobTimer = 0f;
            cameraTransform.localPosition = new Vector3(
                cameraTransform.localPosition.x,
                defaultY,
                cameraTransform.localPosition.z
            );
        }
    }
}
