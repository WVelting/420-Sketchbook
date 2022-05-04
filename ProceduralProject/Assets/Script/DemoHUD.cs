using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;



public class DemoHUD : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        slider = GetComponentInChildren<Slider>();

        if(PlayerPrefs.HasKey("the-volume"))
        {
            slider.value = PlayerPrefs.GetFloat("the-volume", 0);
        }
        slider.onValueChanged.AddListener(OnSliderChange);
    }

    void Update()
    {
        
    }

    public void OnSliderChange(float val)
    {
        //print("slider");
        //TODO: store in PlayerPrefs:
        PlayerPrefs.SetFloat("the-volume", val);
        print($"value saved {val}");
    }

    public void OnButtonSave()
    {
        SaveData data = new SaveData();
        data.playerHealth = 42;
        data.playerLocation = 2;
        data.playerName = "William";

        FileStream stream = File.OpenWrite("savegame.dagd420");

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(stream, data);

        stream.Close();
    }

    public void OnButtonLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = null;
        try
        {
        stream = File.OpenRead("savegame.dagd420");

        }
        catch (System.Exception e)
        {
            print(e);
            return;
        }
        
        SaveData data = null;
        try
        {
            data = (SaveData) bf.Deserialize(stream);

        } 
        catch(System.Exception e)
        {
            print(e);
        }
        stream.Close();

        if(data != null)
        {
            print(data.playerHealth);
            print(data.playerLocation);
            print(data.playerName);
        }


    }
}
