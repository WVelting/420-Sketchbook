using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAgent : MonoBehaviour
{

    static float G = 1;
    static float maxForce = 20;
    static List<GravityAgent> agents = new List<GravityAgent>();
    static void FindGravityForce(GravityAgent a, GravityAgent b)
    {
        if(a == b) return;
        if(a.hasUpdated || b.hasUpdated) return;
        
        Vector3 disToB = b.position - a.position;
        float rr = disToB.sqrMagnitude;
        float gravity = G*(a.mass * b.mass)/rr;

        if(gravity > maxForce) gravity = maxForce;

        disToB.Normalize();

        a.AddForce(disToB * gravity);
        b.AddForce(-disToB * gravity);
    }


    Vector3 position;
    Vector3 force;
    Vector3 velocity;

    float mass;
    bool hasUpdated = false;

    void Start()
    {
        position = transform.position;
        mass = Random.Range(10, 100);
        
        agents.Add(this);

    }

    void OnDestroy()
    {
        agents.Remove(this);
    }

    public void AddForce(Vector3 f)
    {
        force+= f;
    }

    void Update()
    {
        //find gravity to every other agent:
        foreach(GravityAgent a in agents){
            FindGravityForce(this, a);

        }
        hasUpdated = true;

        //euler integration:
        Vector3 acceleration = force/mass;
        

        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        transform.position = position;

        
    }

    private void LateUpdate(){
        hasUpdated = false;
        force *= 0;
    }

}
