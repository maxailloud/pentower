using UnityEngine;
using System.Collections;

[AddComponentMenu("Pentower/Units/TankCollision")]
public class TankCollision : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		//string tag = other.gameObject.tag;
		Tower tower = other.transform.parent.gameObject.GetComponent<Tower> ();
		/*if (tag.Equals ("EnterPoint")) {
			this.gameObject.transform.rotation = Quaternion.LookRotation (-other.gameObject.transform.forward, Vector3.up);
		}
		else*/ 
		if (null == tower || (null != tower && this.gameObject.GetComponent<Unit> ().tower != tower)) {
			this.gameObject.GetComponent<LocomotionController>().enabled = false;
			this.gameObject.rigidbody.velocity = Vector3.zero;
			this.gameObject.rigidbody.angularVelocity = Vector3.zero;
		}

	}
}
