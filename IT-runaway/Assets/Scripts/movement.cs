using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleFPSController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float sprintSpeed = 12f;
    public float gravity = -9.81f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 200f;

    [Header("Stamina Settings")]
    public float maxStamina = 5f; 
    public float staminaDrainRate = 1f; 
    public float staminaRegenRate = 0.75f;
    public float regenDelay = 1f;
    private float stamina;
    private float regenTimer;

    private float xRotation = 0f;
    private float verticalVelocity = 0f;

    private CharacterController controller;
    private Transform cameraTransform;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        stamina = maxStamina;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Mouse Look ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // --- Movement Input ---
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift);
        bool isMoving = x != 0 || z != 0;

        // --- Sprint Logic + Stamina ---
        bool canSprint = stamina > 0f;

        bool isSprinting = wantsToSprint && controller.isGrounded && isMoving && canSprint;

        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        if (isSprinting)
        {
            stamina -= staminaDrainRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
            regenTimer = regenDelay;
        }
        else
        {
            if (regenTimer > 0f)
            {
                regenTimer -= Time.deltaTime;
            }
            else
            {
                stamina += staminaRegenRate * Time.deltaTime;
                stamina = Mathf.Clamp(stamina, 0f, maxStamina);
            }
        }

        // --- Gravity ---
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    public float GetStamina01()
    {
    
        return stamina / maxStamina;
    }
}
