using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

     float xRotation;
     float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //mouse input
        float mouseX = Input.GetAxisRaw("Mouse X")  * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y")  * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate camera
        transform.rotation = Quaternion.Euler(xRotation, yRotation,0);
        
        // makes shure the orientation is the correct rotation in Y 
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
