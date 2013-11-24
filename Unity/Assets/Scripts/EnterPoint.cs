using UnityEngine;
using System.Collections;

public class EnterPoint : MonoBehaviour
{
	public Transform target;
	
	void OnDestroy()
	{
		this.target = null;
	}

	void OnTriggerEnter (Collider other)
	{
		Tower tower = this.gameObject.transform.parent.GetComponent<Tower> ();
		Unit unit = other.transform.parent.parent.parent.GetComponent<Unit> ();
		if (null != unit && null != tower && tower == unit.tower) {
			unit.transform.rotation = Quaternion.LookRotation (target.position - unit.transform.position, Vector3.up);
		}
	}
}

