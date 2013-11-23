using UnityEngine;
using System.Collections;

public class EnterPoint : MonoBehaviour
{
	public Tower target;

	void OnTriggerEnter (Collider other)
	{
		Unit unit = other.transform.parent.parent.parent.GetComponent<Unit> ();
		if (null != unit) {
			Debug.Log("Reached enter point "+target.tag);
			Debug.Log(target.transform.position);
			unit.transform.rotation = Quaternion.LookRotation (target.transform.position - unit.transform.position, Vector3.up);
		}
		
	}
}

