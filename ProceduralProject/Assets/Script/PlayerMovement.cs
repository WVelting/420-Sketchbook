using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // TODO: Add Jumping

    // Add variables to store movement speed, and how fast the player can look around.
    public float moveSpeed = 4;
    public float sensitivityX = 50;
    public float sensitivityY = -50;

    // Hold the camera, and it's angle.
    private Camera cam;
    float cameraAngle = 0;

    // Store the character as a pawn.
    public CharacterController pawn;

    private void Start()
    {
        // Lock the mouse to the ends of the screen.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Get the controller, and camera components.
        pawn = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();

    }

    private void Update()
    {
        // Move the player in a direction every tick.
        MovePlayer();
        // Turn the player based on mouse movement every tick.
        TurnCamera();
    }
    void MovePlayer()
    {
        // Get the player's current location 
        float v = Input.GetAxis("Vertical"); // W + S up and down
        float h = Input.GetAxis("Horizontal"); // A + D left and right
        float j = 10;

        // Use the player's current location to move them by speed on a vector.
        //transform.position += (transform.right * h + transform.forward * v) * speed * Time.deltaTime;
        Vector3 speed = (transform.right * h + transform.forward * v) * moveSpeed;
        //pawn.SimpleMove(speed);


        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            Debug.Log("Jump");
            Vector3 jump = (transform.right * h + transform.forward *v + transform.up * j) * moveSpeed;
            
            pawn.Move(jump);
        }
        
        pawn.SimpleMove(speed);


        // Move the player along the vector.
       

    }
    void TurnCamera()
    {
        // Store mouse location within variables
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        // Use h to transform the camera's rotation
        transform.Rotate(0, h * sensitivityX, 0);

        // Use v to add to the camera's pitch degree
        cameraAngle += v * sensitivityY;

        // Clamp the values so that the mouse doesn't go off of the screen.
        if (cameraAngle < -80) cameraAngle = -80;
        if (cameraAngle > 80) cameraAngle = 80;

        cam.transform.localRotation = Quaternion.Euler(cameraAngle, 0, 0);
    }
}
