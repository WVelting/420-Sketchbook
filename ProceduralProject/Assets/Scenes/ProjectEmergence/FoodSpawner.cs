using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public Transform foodSpawner;
    public Transform food;
    public Boid boid;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            boid.AddFood(Instantiate(food, transform.position, Quaternion.identity).GetComponent<Food>());
        }
    }
    
}
