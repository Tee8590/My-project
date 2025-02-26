
using UnityEngine;

// Manages and coordinates the other classes
public class PushManager : MonoBehaviour
{
    public GameObject pushReady;
    private PushDetector pushDetector;
    private ObjectMover objectMover;
    private ImpulseApplier impulseApplier;
    private ItemSpawner itemSpawner;

    private Vector3 rasePos;
    private void Start()
    {
        pushDetector = GetComponent<PushDetector>();
        objectMover = GetComponent<ObjectMover>();
        impulseApplier = GetComponent<ImpulseApplier>();
        itemSpawner = GetComponent<ItemSpawner>();

        // Spawn initial items
        itemSpawner.SpawnItems(10);
        Vector3 rasePos = new Vector3(0, transform.position.y+5, 0);
    }

    private void FixedUpdate()
    {
        if (pushReady != null)
        {
            pushDetector.DetectObjects(pushReady.transform.position, 10);
        }
        if (Input.GetKey(KeyCode.R))
        {
            itemSpawner.SpawnItems(pushDetector.DetectedItems.Count); // Ensure objects are available
            StartCoroutine(objectMover.SmoothRaise(pushDetector.DetectedItems, rasePos, 1.5f));
        }
        if (Input.GetKey(KeyCode.T))
        {
            impulseApplier.ApplyForwardImpulse(pushDetector.DetectedItems, 5);
        }
    }
}
