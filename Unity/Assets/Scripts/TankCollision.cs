using UnityEngine;
using System.Collections;

[AddComponentMenu("Pentower/Units/TankCollision")]
public class TankCollision : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		Tower tower = other.transform.parent.gameObject.GetComponent<Tower> ();
		if (null != tower && this.gameObject.GetComponent<Unit> ().tower != tower) {
			Destroy(this.gameObject);
			/*
			this.gameObject.GetComponent<LocomotionController>().enabled = false;
			this.gameObject.rigidbody.velocity = Vector3.zero;
			this.gameObject.rigidbody.angularVelocity = Vector3.zero;
			*/
		}
	}
}
