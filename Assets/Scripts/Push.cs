using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public GameObject pushReady = null;
    public List<Transform> items = new List<Transform>();
    private int layerMask = 0;
    public Animations animations;
    public GameObject pushDirection;
    Dictionary<Transform, Vector3> startPositions;
    Dictionary<Transform, Vector3> targetPositions;
    public void Start()
    {
        layerMask = ~LayerMask.GetMask("ground", "player");
    }

    private void FixedUpdate()
    {
        if (pushReady != null)
        {
            AllMightyPush(pushReady.transform.position, 10);
        }
        if (Input.GetKey(KeyCode.R))
        {
            animations.H1Anim();
            StartCoroutine(SmoothRase());
        }
        else
        {
            animations.IdleAnim();
        }

        if (Input.GetKey(KeyCode.T))
        {
            animations.H2Anim();
            ApplyForwardImpulse(5);

        }
        else
        {
            animations.IdleAnim();
        }
    }

    public void AllMightyPush(Vector3 center, float radius)
    {
        int maxColliders = 10;
        Collider[] hits = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(center, radius, hits, layerMask);

        for (int i = 0; i < numColliders; i++)
        {
            Transform hitTransform = hits[i].GetComponent<Transform>();
            if (hitTransform != null && !items.Contains(hitTransform))
            {
                items.Add(hitTransform);
            }
        }
    }


    private IEnumerator SmoothRase()
    {
        float duration = 1.5f; // Time taken to move objects
        float elapsedTime = 0f;

        startPositions = new Dictionary<Transform, Vector3>();
        targetPositions = new Dictionary<Transform, Vector3>();
        // Create a local copy of items to prevent modification during iteration
        List<Transform> itemsSnapshot = new List<Transform>(items);

        foreach (Transform item in itemsSnapshot)
        {
            
            if (item != null)
            {
                startPositions[item] = item.position;
                targetPositions[item] = new Vector3(
                     Random.Range(pushReady.transform.position.x, pushReady.transform.position.x + 2f),
                     Random.Range(pushReady.transform.position.y, pushReady.transform.position.y + 2f),
                     Random.Range(pushReady.transform.position.z, pushReady.transform.position.z + 2f));
                   /* new Vector3(
                 item.position.x,
                pushReady.transform.position.y,
                 item.position.z
                pushReady.transform.position.x,
                pushReady.transform.position.y,
                pushReady.transform.position.z
                );*/
            }
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            foreach (Transform item in itemsSnapshot)
            {
                if (item != null)
                {
                    item.position = Vector3.Lerp(startPositions[item], targetPositions[item], t);
                }
            }

            yield return null;
        }

        foreach (Transform item in itemsSnapshot)
        {
            if (item != null)
            {
                item.position = targetPositions[item]; // Ensure final position is exact
            }
        }
        items.Clear();
    }
    public void ApplyForwardImpulse(float forceAmount)
    {
        if (startPositions == null) return;
        List<Transform> toRemove = new List<Transform>();

        foreach (Transform item in startPositions.Keys)
        {
            if (item != null)
            {

                Rigidbody rb = item.GetComponent<Rigidbody>();// Get Rigidbody component

                if (rb != null)
                {
                    Vector3 direction = pushDirection.transform.position ;  /*Vector3 pushDirection = (item.position - pushReady.transform.position).normalized;*/ // Direction away from pushReady
                    rb.AddForce((direction + Vector3.forward ) * forceAmount* Time.deltaTime, ForceMode.Impulse); // Apply impulse force
                    toRemove.Add(item);
                }
            }
        }

    }

}
