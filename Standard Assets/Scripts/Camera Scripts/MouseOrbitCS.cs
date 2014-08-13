using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit C#")]

public class MouseOrbitCS :MonoBehaviour
{
    public Transform targetObject;
    public float distance = 10.0f;

    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    public int yMinLimit = -20;
    public int yMaxLimit = 80;

    private float x = 0.0f;
    private float y = 0.0f;


    void Start () {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

	    // Make the rigid body not change rotation
   	    if (GetComponent<Rigidbody>())
        {
		    GetComponent<Rigidbody>().freezeRotation = true;
        }

    }

    void LateUpdate () 
    {
        if (targetObject) 
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
 		
 		    y = ClampAngle(y, yMinLimit, yMaxLimit);
 		       
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + targetObject.position;
        
            transform.rotation = rotation;
            transform.position = position;
        }

    }

    static float ClampAngle (float angle, float min, float max) 
    {
        if (angle < -360)
        {
            angle += 360;
        }

        if (angle > 360)
        {
            angle -= 360;
        }

	    return Mathf.Clamp (angle, min, max);
    }
}