using UnityEngine;

public class FOVSprint : MonoBehaviour
{
    [Header("FOV Settings")]
    public float normalFOV = 70f;
    public float sprintFOV = 80f;
    public float fovChangeSpeed = 8f;

    [Header("References")]
    public CharacterController controller;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (controller == null)
            controller = GetComponent<CharacterController>();

        cam.fieldOfView = normalFOV;
    }

    void Update()
    {
        // Check if sprint key is down AND grounded
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && controller.isGrounded;

        float targetFOV = isSprinting ? sprintFOV : normalFOV;

        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView,
            targetFOV,
            Time.deltaTime * fovChangeSpeed
        );
    }
}
