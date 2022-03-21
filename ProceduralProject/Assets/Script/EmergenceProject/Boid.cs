using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum boidTypes
{
    Lil,
    Big,
    ReallyBig
}

[RequireComponent(typeof(Rigidbody))]
public class Boid : MonoBehaviour
{

    public boidTypes type;

    private Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        BoidManager.AddBoid(this);
        
    }

    void OnDestroy()
    {
        BoidManager.RemoveBoid(this);
    }

    void Update()
    {
        
    }

    void CalcForces(Boid[] boids)
    {

        BoidSettings settings = BoidManager.GetSettings(type);
        //Alignment
        //Cohesion
        //Separation


        foreach(Boid b in boids)
        {
            

            Vector3 vToOther = b.transform.position - transform.position;
            float d = vToOther.magnitude;

            if(d < settings.radiusAlignment)
            {

            }
            if(d < settings.radiusCohesion)
            {

            }
            if(d < settings.radiusSeparation)
            {
                Vector3 separation = (-vToOther/d) * (settings.forceSeparation/d) * Time.deltaTime;
                body.AddForce(separation);
            }
        }

        //TODO:
        //apply alignment steering force
        //apply cohesion steering force
    }
}
