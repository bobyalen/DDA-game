using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float x, y;
    float xRotation, yRotation;

    public Transform direction;
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxisRaw("Mouse X") * Time.deltaTime * x;
        float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * y;

        yRotation += mousex;
        xRotation -= mousey;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        direction.rotation = Quaternion.Euler(0,yRotation, 0);

    }
}
