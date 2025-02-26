using UnityEngine;
// Responsible for spawning objects
public class ItemSpawner : MonoBehaviour
{
    public void SpawnItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject itemObj = Item.Instance.GetItemsToPush();
            if (itemObj != null)
            {
                itemObj.SetActive(false);
                itemObj.transform.position = Item.Instance.SpawnPosition();
                itemObj.transform.rotation = Item.Instance.RandomRotation();
                itemObj.SetActive(true);
            }
        }
    }
}
