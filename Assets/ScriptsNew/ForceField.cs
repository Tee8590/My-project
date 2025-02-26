using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ForceField : MonoBehaviour
{
    public GameObject filedObj;
    private Vector3 fieldPos;
    public LayerMask layerMask;

    private Collider[] hitColliders;
    private int numColliders;

    private float elapsTime = 0f;
    private int duriation = 5;
    void Start()
    {
        
        /*filedObj = GameObject.Fi("filedObj");*/
    }

    // Update is called once per frame
    void Update()
    {
        fieldPos = filedObj.transform.position;
        ForceFealdDetect(fieldPos, 5f);
    }
    public void ForceFealdDetect(Vector3 center, float radius)
    {
        int maxColliders = 10;
         hitColliders = new Collider[maxColliders];
        numColliders = Physics.OverlapSphereNonAlloc(center, radius, hitColliders, layerMask);
        for (int i = 0; i < numColliders; i++)
        {
            if (Input.GetMouseButton(0))
            {

                //Debug.Log(hitColliders[i]);
                StartCoroutine(ApplayRaiseF(i));
                ApplayRaiseF(i);
            }
        }
    }
    public IEnumerator ApplayRaiseF(int hit)
    {

        if (hitColliders[hit] != null)
        {
            elapsTime = 0f;   
                while (elapsTime < duriation)
                {
                    elapsTime += Time.deltaTime;
                    float t = elapsTime / duriation;

                    for (int j = 0; j < numColliders; j++)
                    {
                        Vector3 objPos = hitColliders[j].gameObject.transform.position;
                        
                        Vector3 endPos = new Vector3(hitColliders[j].gameObject.transform.position.x, (hitColliders[j].gameObject.transform.position.y + 2), hitColliders[j].gameObject.transform.position.z);
                        Vector3 forceUp = Vector3.Lerp(objPos, endPos, t);
                        
                        hitColliders[j].gameObject.transform.position = forceUp;
                    }
                    yield return null;

                }
            

        }
       
    }
}
