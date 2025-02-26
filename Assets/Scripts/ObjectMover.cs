using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Responsible for smoothly moving objects upwards
public class ObjectMover : MonoBehaviour
{
    public IEnumerator SmoothRaise(List<Transform> items, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        Dictionary<Transform, Vector3> startPositions = new Dictionary<Transform, Vector3>();

        foreach (Transform item in items)
        {
            if (item != null && item.gameObject.activeInHierarchy)
                startPositions[item] = item.position;
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            foreach (Transform item in items)
            {
                if (item != null && startPositions.ContainsKey(item))
                {
                    Vector3 newPosition = Vector3.Lerp(startPositions[item], targetPosition, t);
                    item.position = newPosition;
                }
            }
            yield return null;
        }
    }
}