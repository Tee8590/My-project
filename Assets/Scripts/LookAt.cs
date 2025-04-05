using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Camera cam = null;
    private Vector3 camForward;
    public float rotationSpeed = 10f;
    public CharacterController controller = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateTowardsCamera();
    }
    void RotateTowardsCamera()
    {
        if (cam != null)
        {
            camForward = cam.transform.forward;
            /*camForward.y += camForward.y;*/
            Quaternion targetRotation = Quaternion.LookRotation(camForward);
            Quaternion playerRotation = controller.transform.rotation;
            controller.transform.rotation = Quaternion.Slerp(playerRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
