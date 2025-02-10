using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject itemPrefab = null;
    public GameObject Player = null;
    public static Item Instance;
    public List<GameObject> itemsToPush = new List<GameObject>();
    public int amountOfObj = 80;
    private Vector3 position;
    private Quaternion rotation;
    private void Awake()
    {
        if (Instance == null)
           
            Instance = this;
       /* Debug.Log(Instance);*/
    }
    void Start()
    {
        if (itemPrefab == null)
            Debug.Log("item null");
       
        for (int i = 0; i < amountOfObj; i++)
        {
            GameObject item = Instantiate(itemPrefab, position,rotation);
           /* Debug.Log("item");*/
            item.gameObject.SetActive(false);
            itemsToPush.Add(item);
            /*Debug.Log("itemsToPush");*/
        }
    }
    public void FixedUpdate()
    {
        position = SpawnPosition();
        rotation = RandomRotation();
    }
    public GameObject GetItemsToPush()
    {
        for(int i = 0;i < itemsToPush.Count;i++)
        {
            if (!itemsToPush[i].gameObject.activeInHierarchy)
            {
                return itemsToPush[i];
            }
        }
        return null;
    }
    public Vector3 SpawnPosition()
    {
       
        position.y = 1f;
        position.x = Player.transform.position.x + Random.Range(Player.transform.position.x , Player.transform.position.x-5f);
        position.z = Player.transform.position.z + Random.Range(Player.transform.position.z , Player.transform.position.z+5f);
        return position;
    }
    public Quaternion RandomRotation()
    {
        float x = Random.Range(0f, 360f);
        float y = Random.Range(0f, 360f);
        float z = Random.Range(0f, 360f);
        return Quaternion.Euler(x, y, z);
        
    }

}
