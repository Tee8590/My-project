using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public GameObject pushReady = null;
    public List<Transform> items = new List<Transform>();
    private int layerMask = 0;
  /*  public Dictionary<Transform, Vector3> targetPositions = new Dictionary<Transform, Vector3>();*/
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
            StartCoroutine(SmoothRase());
        if (Input.GetKey(KeyCode.T))
            ApplyForwardImpulse(5);

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
        float duration = 2f; // Time taken to move objects
        float elapsedTime = 0f;

        Vector3[] startPositions = new Vector3[items.Count]; // Store start positions
        Vector3[] targetPositions = new Vector3[items.Count];

        for (int i =0; i<items.Count;i++)
        {
            if (i != null)
            {
                startPositions[i] = items[i].position;
                targetPositions[i] = new Vector3(
                    items[i].position.x,
                    items[i].position.y+5,
                    items[i].position.z
                   /* pushReady.transform.position.x,
                    pushReady.transform.position.y ,
                    pushReady.transform.position.z*/
                );
            }
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            for(int i =0;i <items.Count;i++)
            {
                if (i != null)
                {
                    items[i].position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
                }
            }

            yield return null;
        }

        for(int i =0;i< items.Count; i++)
        {
            if (i != null)
            {
                items[i].position = targetPositions[i]; // Ensure final position is exact
            }
        }
    }
    public void ApplyForwardImpulse(float forceAmount)
    {
        foreach (Transform item in items)
        {
            if (item != null)
            {

                Rigidbody rb = item.GetComponent<Rigidbody>(); // Get Rigidbody component

                if (rb != null)
                {
                    Vector3 pushDirection = Vector3.forward;   /*Vector3 pushDirection = (item.position - pushReady.transform.position).normalized;*/ // Direction away from pushReady
                    rb.AddForce(pushDirection * forceAmount, ForceMode.Impulse); // Apply impulse force
                }
            }
        }
    }
    public void ClearObj()
    {
      /*  targetPositions.Clear();*/
    }

}
