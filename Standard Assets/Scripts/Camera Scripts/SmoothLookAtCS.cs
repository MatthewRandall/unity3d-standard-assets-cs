using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Smooth LookAt C#")]

public class SmoothLookAtCS : MonoBehaviour
{
    public Transform targetObject;
    public float damping = 6.0f;
    public bool smooth = true;

    void LateUpdate () 
    {
        if (targetObject)
        {
		    if (smooth)
		    {
			    // Look at and dampen the rotation
                Quaternion rotation = Quaternion.LookRotation(targetObject.position - transform.position);
			    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
		    }
		    else
		    {
			    // Just lookat
                transform.LookAt(targetObject);
		    }
	    }
    }

    void Start () 
    {
	    // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

    }
}