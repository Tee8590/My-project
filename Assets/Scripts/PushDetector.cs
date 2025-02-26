using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for detecting objects within a radius
public class PushDetector : MonoBehaviour
{
    public List<float> DetectedYPositions { get; private set; } = new List<float>();
    public List<Transform> DetectedItems { get; private set; } = new List<Transform>();
    private int layerMask;

    private void Start()
    {
        layerMask = ~LayerMask.GetMask("ground", "player");
    }

    public void DetectObjects(Vector3 center, float radius)
    {
        DetectedYPositions.Clear();
        DetectedItems.Clear();
        Collider[] hits = new Collider[10];
        int numColliders = Physics.OverlapSphereNonAlloc(center, radius, hits, layerMask);

        for (int i = 0; i < numColliders; i++)
        {
            Transform hitTransform = hits[i].transform;
            if (hitTransform != null)
            {
                if (!DetectedYPositions.Contains(hitTransform.position.y))
                {
                    DetectedYPositions.Add(hitTransform.position.y);
                }
                DetectedItems.Add(hitTransform);
            }
        }
    }
}
