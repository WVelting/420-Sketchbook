using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoidSettings{
    public boidTypes type;

    public Boid prefab;
    public float maxSpeed;
    public float maxForce;

    public float radiusAlignment;
    public float radiusCohesion;
    public float radiusSeparation;

    public float forceAlignment;
    public float forceCohesion;
    public float forceSeparation;
}

public class BoidManager : MonoBehaviour
{
    public BoidSettings[] settings;

    public static BoidManager singleton;

    private List<Boid> boids = new List<Boid>();

    private void Start()
    {
        if(singleton != null) Destroy(gameObject);
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public static BoidSettings GetSettings(boidTypes type)
    {
        foreach(BoidSettings bs in singleton.settings)
        {
            if(bs.type == type) return bs;
        }
        return new BoidSettings();
    }

    public static void AddBoid(Boid b)
    {
        singleton.boids.Add(b);
    }

    public static void RemoveBoid(Boid b)
    {
        singleton.boids.Remove(b);
    }

    void Update()
    {
        if(boids.Count < 2)
        {
            if(settings.Length> 0)
            {
                Boid b = Instantiate(settings[0].prefab);
                b.type = settings[0].type;
            }
        }
    }
    

}
