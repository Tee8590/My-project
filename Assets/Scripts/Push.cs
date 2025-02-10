using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public GameObject pushReady = null;
    public List<Transform> items = new List<Transform>();
    private GameObject itemObj;
    private int layerMask = 0;

    Dictionary<Transform, Vector3> startPositions;
    Dictionary<Transform, Vector3> targetPositions;
    public void Start()
    {
        layerMask = ~LayerMask.GetMask("ground", "player");
     /*   SpawnItems();*/
    }

    private void FixedUpdate()
    {
        
        if (pushReady != null)
        {
            AllMightyPush(pushReady.transform.position, 10);
            
        }
        if (Input.GetKey(KeyCode.R))
        {   

            StartCoroutine(SmoothRase());
        }
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
                /*Debug.Log("hit");*/
            }
        }
    }


    private IEnumerator SmoothRase()
    {
        float duration = 1.5f; // Time taken to move objects
        float elapsedTime = 0f;

          startPositions = new Dictionary<Transform, Vector3>();
         targetPositions = new Dictionary<Transform, Vector3>();
        SpawnItems();
        foreach (Transform item in items)
        {
            if (item != null)
            {
                startPositions[item] = item.position;
                targetPositions[item] = new Vector3(
                      item.position.x,
                     pushReady.transform.position.y,
                      item.position.z
                   /* pushReady.transform.position.x,
                    pushReady.transform.position.y,
                    pushReady.transform.position.z*/
                );
            }
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            foreach (Transform item in items)
            {
                if (item != null)
                {
                    item.position = Vector3.Lerp(startPositions[item], targetPositions[item], t);
                }
            }

            yield return null;
        }

        foreach (Transform item in items)
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
        if(startPositions==null) return;
        List<Transform> toRemove = new List<Transform> ();

        foreach (Transform item in startPositions.Keys)
        {
            if (item != null)
            {

                Rigidbody rb = item.GetComponent<Rigidbody>();// Get Rigidbody component

                if (rb != null)
                {
                    Vector3 pushDirection = Vector3.forward;   /*Vector3 pushDirection = (item.position - pushReady.transform.position).normalized;*/ // Direction away from pushReady
                    rb.AddForce(pushDirection * forceAmount, ForceMode.Impulse); // Apply impulse force
                    itemObj.gameObject.SetActive(false);
                    /*toRemove.Add(item);*/
                }
            }
        }
       
    }
    private void SpawnItems()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject itemObj = Item.Instance.GetItemsToPush();
            if (itemObj != null)
            {
                itemObj.SetActive(false); // Ensure it's inactive before setting properties
                itemObj.transform.position = Item.Instance.SpawnPosition();
                itemObj.transform.rotation = Item.Instance.RandomRotation();
                itemObj.SetActive(true);
            }
        }
    }
}
