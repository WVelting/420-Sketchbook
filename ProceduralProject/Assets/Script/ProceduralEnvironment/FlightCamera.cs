using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightCamera : MonoBehaviour
{

    [Range(1, 10)]
    public float scalar = 3;
    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;

    private float pitch = 0;
    private float yaw = 0;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.LeftShift)) scalar *= 2;
        if(Input.GetKeyUp(KeyCode.LeftShift)) scalar /= 2;
        


        //Update Position-----------------------
        float h = Input.GetAxis("Horizontal"); // right/left
        float v = Input.GetAxis("Vertical"); // forward/back
        float u = Input.GetAxis("Jump"); // up(q)/down(z)

        Vector3 dir = transform.forward * v + transform.right * h + transform.up * u;
        if (dir.magnitude > 1) dir.Normalize();
        transform.position += dir * scalar * Time.deltaTime;

        //Update Rotation--------------------------
        float lookRight = Input.GetAxisRaw("Mouse X"); //yaw (Y)
        float lookUp = Input.GetAxisRaw("Mouse Y"); //pitch (X)

        yaw += lookRight * mouseSensitivityY;
        pitch -= lookUp * mouseSensitivityX;

        pitch = Mathf.Clamp(pitch, -89, 89);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
