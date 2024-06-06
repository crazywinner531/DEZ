using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;

    private Rigidbody rb;
    private Camera playerCamera;

    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        // Lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Freeze rotation along the X and Z axes
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // Mouse input for looking around
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Keyboard input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on camera forward direction
        Vector3 forwardDirection = playerCamera.transform.forward;
        forwardDirection.y = 0; // Ensure no movement in the y-direction
        forwardDirection.Normalize();

        // Calculate movement direction based on camera right direction
        Vector3 rightDirection = playerCamera.transform.right;
        rightDirection.y = 0; // Ensure no movement in the y-direction
        rightDirection.Normalize();

        // Combine movement directions based on input
        Vector3 moveDirection = (verticalInput * forwardDirection + horizontalInput * rightDirection).normalized;
        Vector3 moveVelocity = moveDirection * moveSpeed;

        // Apply movement velocity to the rigidbody
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }
}
