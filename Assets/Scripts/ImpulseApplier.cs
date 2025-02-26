using System.Collections.Generic;
using UnityEngine;

// Responsible for applying an impulse force to objects
public class ImpulseApplier : MonoBehaviour
{
    public void ApplyForwardImpulse(List<Transform> items, float forceAmount)
    {
        foreach (Transform item in items)
        {
            if (item != null)
            {
                Rigidbody rb = item.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 pushDirection = Vector3.forward;
                    rb.AddForce(pushDirection * forceAmount, ForceMode.Impulse);
                }
               /* item.gameObject.SetActive(false);*/ // Disable the object after applying impulse
            }
        }
    }
}

