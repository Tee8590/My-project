using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    public Camera cam = null;
    private Vector3 camForward;
    public float rotationSpeed = 10f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        isGrounded = controller.isGrounded;
        RotateTowardsCamera();
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void RotateTowardsCamera()
    {
        if (cam != null )
        {
            camForward = cam.transform.forward;
            /*camForward.y += camForward.y;*/
            Quaternion targetRotation = Quaternion.LookRotation(camForward);
            Quaternion playerRotation = controller.transform.rotation;
            controller.transform.rotation = Quaternion.Slerp(playerRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
