using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{

    public Slider health;
    public Slider time;
    public CastleScript castle;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        health.value = castle.health;
        time.value = castle.gameTimer;
  
    }
}
