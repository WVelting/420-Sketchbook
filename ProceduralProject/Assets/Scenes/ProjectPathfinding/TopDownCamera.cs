using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TopDownCamera : MonoBehaviour
{
    private CharacterController cam;
    public float moveSensitivity = 3;
    void Start()
    {
        cam = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 input = transform.forward*v + transform.right*h;
        cam.Move(input*Time.deltaTime*moveSensitivity);
        
        
    }
}
