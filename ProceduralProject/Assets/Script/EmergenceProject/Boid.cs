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

    public void CalcForces(Boid[] boids)
    {

        BoidSettings settings = BoidManager.GetSettings(type);
        //Alignment
        //Cohesion
        //Separation

        Vector3 avgCenter = Vector3.zero;
        int numCohesion = 0;


        foreach(Boid b in boids)
        {
            

            Vector3 vToOther = b.transform.position - transform.position;
            float d = vToOther.magnitude;

            if(d < settings.radiusAlignment)
            {

            }
            if(d < settings.radiusCohesion)
            {
                avgCenter += b.transform.position;
                numCohesion++;

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
        if(numCohesion > 0) 
        {
            avgCenter /= numCohesion;

            Vector3 vToCenter = avgCenter - transform.position;
            Vector3 desiredVelocity = vToCenter.normalized * settings.maxSpeed;

            Vector3 forceCohesion = desiredVelocity - body.velocity;

            if(forceCohesion.sqrMagnitude > Mathf.Pow(settings.maxForce, 2)) forceCohesion = forceCohesion.normalized * settings.maxForce;


            body.AddForce(forceCohesion * settings.forceCohesion * Time.deltaTime);
        }
    }
}
