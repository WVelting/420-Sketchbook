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
    private BoidSettings settings;
    public List<Food> food = new List<Food>();
    public float thisBoidLife;

    

    void Start()
    {
        body = GetComponent<Rigidbody>();
        BoidManager.AddBoid(this);
        settings = BoidManager.GetSettings(type);
        thisBoidLife = settings.lifeSpan;

        
    }

    void OnDestroy()
    {
        BoidManager.RemoveBoid(this);
    }

    void Update()
    {
        thisBoidLife -= Time.deltaTime;
        if(thisBoidLife<= 0) Destroy(gameObject);
    }

    public void AddFood(Food f)
    {
        food.Add(f);
    }

    public void RemoveFood(Food f)
    {
        food.Remove(f);
    }

    public void CalcForces(Boid[] boids)
    {

        if(food.Count>0)
        {
            float foodCounter = food.Count;
            Vector3 avgFood = Vector3.zero;
            foreach(Food v in food)
            {
                avgFood += v.transform.position;
            }
            Vector3 vToFood = avgFood/foodCounter;
            body.AddForce(vToFood * settings.forceCohesion * Time.deltaTime);

            // foreach(Food v in food)
            // {
            //     if((v.transform.position - transform.position).sqrMagnitude < 6)
            //     {
            //         RemoveFood(v);
            //         Destroy(v.gameObject);
            //         thisBoidLife += 20;
            //     }
            // }
        }
        else
        {

        }
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
                if(d < settings.radiusSeparation && d != 0)
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
