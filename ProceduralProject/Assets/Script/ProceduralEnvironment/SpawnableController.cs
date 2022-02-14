using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableController : MonoBehaviour
{

    public GameObject[] prefabs;
    private int whichObject;
    // Start is called before the first frame update
    void Start()
    {
        whichObject = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[whichObject], transform.position, transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
