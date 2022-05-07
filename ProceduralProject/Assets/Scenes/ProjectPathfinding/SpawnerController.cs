using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public static SpawnerController singleton;
    public PeonController prefab;
    public List<PeonController> minions = new List<PeonController>();
    public float spawnTimer = .5f;
    public float scalar = 10;
    void Start()
    {
        if(singleton == null) singleton = this;
        else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(singleton.minions.Count < 10 && spawnTimer <= 0)
        {   
            Instantiate(prefab, new Vector3(Random.Range(10,30), .5f, Random.Range(25,35)), Quaternion.identity);
            spawnTimer = .5f;
        }
        
    }

    public static void AddMinion(PeonController m)
    {
        singleton.minions.Add(m);
    }
    public static void RemoveMinion(PeonController m)
    {
        singleton.minions.Remove(m);
    }
}
