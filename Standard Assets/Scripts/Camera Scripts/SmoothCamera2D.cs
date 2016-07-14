using UnityEngine;
using System.Collections;

/// <summary>
/// <para>
/// Follow the target with Smooth X-axis like a 2D camera. 
/// This script is simple and works to platformer/side-scrolling games, 3D or 2D.
/// </para>
/// <para>
/// 
/// </para>
/// <para>
/// Based on script written by Scott Kovacs via UnityAnswers.com; Oct 5th 2010
/// </para>
/// <para>
/// http://answers.unity3d.com/questions/29183/2d-camera-smooth-follow.html
/// </para>
/// </summary>
/// 
[RequireComponent(typeof(Camera)), DisallowMultipleComponent]
public class SmoothCamera2D : MonoBehaviour {
	
	#region public params

	/// <summary>
	/// The target that camera will follow
	/// </summary>
	[Tooltip("The target that camera will follow")]
	public Transform target;

	/// <summary>
	/// Seconds to damp a smooth camera
	/// </summary>
	[Tooltip("Seconds to damp a smooth camera")]
	public float dampTime = 0.15f;

	/// <summary>
	/// Replaces the original camera y-axis
	/// </summary>
	[Tooltip("Replaces the original camera y-axis")]
	public float positionY = 0.0f;

	#endregion

	#region internal members

	/// <summary>
	/// The velocity to the smooth. Used internally
	/// </summary>
	protected Vector3 velocity = Vector3.zero;

	/// <summary>
	/// Camera component reference
	/// </summary>
	protected Camera cam;

	#endregion

	void Start()
	{
		if (!target) {
			throw new MissingReferenceException ("The 'target' param needs be defined to camera follow");
		}
		cam = GetComponent<Camera>();

	}

	// Update is called once per frame
	void FixedUpdate ()
	{

		if (target)
		{
			FollowSmooth ();

		}

	}

	void FollowSmooth ()
	{
		Vector3 point = cam.WorldToViewportPoint(target.position);
		Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
		Vector3 destination = transform.position + delta;

		// Set this to the Y position you want to the camera
		if (positionY > 0) {
			destination.y = positionY;	
		} else {
			destination.y = transform.position.y;
		}


		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
	}
}