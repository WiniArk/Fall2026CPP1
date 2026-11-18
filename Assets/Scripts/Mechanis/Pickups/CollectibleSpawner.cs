using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    //Assign collectible prefabs and five spawn points in the Inspector
    [SerializeField] private GameObject[] collectiblePrefabs;
    [SerializeField] private Transform[] spawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    // Stop if no collectible prefabs have been assigned
     if (collectiblePrefabs.Length == 0)
     {
        Debug.LogError("No collecttible prefabs assigned");
        return;
     }   
    
     // Pick an index from 0 up to, but excluding, the array length
     int randomIndex = Random.Range(0, collectiblePrefabs.Length);

     //Spawn one random collectible at each spawnpoint 
     foreach (Transform spawnPoint in spawnPoints)
     {

        // Create the collectible at this position with no rotation
        Instantiate(collectiblePrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
     }

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
