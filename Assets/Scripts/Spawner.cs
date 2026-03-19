using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance; //chance to spawn this object (0-1)
    }
    public SpawnableObject[] spawnableObjects; //array of objects that can be spawned
    public float minSpawnInterval = 1f; //time between spawns
    public float maxSpawnInterval = 2f;
    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnInterval, maxSpawnInterval)); //start the spawning loop
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    void Spawn()
    {
        float randomSpawnChance = Random.value; //random value between 0 and 1
        foreach (var spawnable in spawnableObjects)
        {
            if (randomSpawnChance < spawnable.spawnChance)
            {
                GameObject spawnedObject = Instantiate(spawnable.prefab, transform.position, Quaternion.identity); //spawn the object
                break; //only spawn one object per interval
            }
            randomSpawnChance -= spawnable.spawnChance; //decrease the chance for the next object
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnInterval, maxSpawnInterval)); //schedule the next spawn
    }
}
