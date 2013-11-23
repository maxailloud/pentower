using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class LocomotionController : MonoBehaviour
{
	public float initialVelocity = 5.0f;
	
	// Update is called once per frame
	void Update ()
	{
		rigidbody.velocity = this.initialVelocity * transform.forward;
	}
}

